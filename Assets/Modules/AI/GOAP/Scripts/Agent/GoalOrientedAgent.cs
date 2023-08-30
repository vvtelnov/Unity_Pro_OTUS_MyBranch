using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AI.GOAP
{
    [AddComponentMenu("AI/GOAP/Goal Oriented Agent")]
    [DisallowMultipleComponent]
    public class GoalOrientedAgent : MonoBehaviour, IActor.Callback
    {
        public event Action OnStarted;
        public event Action OnFailed;
        public event Action OnCanceled;
        public event Action OnCompleted;

        [SerializeField, PropertyOrder(-11), HideInPlayMode]
        private PlannerMode plannerMode;

        [SerializeField, Space, PropertyOrder(-10), HideInPlayMode]
        private WorldState worldState;

        [SerializeField, Space, PropertyOrder(-9), HideInPlayMode]
        private Goal[] goals;

        [SerializeField, Space, PropertyOrder(-8), HideInPlayMode]
        private Actor[] actions;

        private IPlanner planner;

        [Title("Debug")]
        [ShowInInspector, ReadOnly, PropertySpace, PropertyOrder(-7)]
        public bool IsPlaying
        {
            get { return this.currentPlan != null; }
        }

        [ShowInInspector, ReadOnly, PropertyOrder(-6)]
        public IGoal CurrentGoal
        {
            get { return this.currentGoal; }
        }

        public IEnumerable<IGoal> Goals
        {
            get { return this.goals; }
        }

        public IEnumerable<IActor> Actions
        {
            get { return this.actions; }
        }

        private IGoal currentGoal;

        [ShowInInspector, ReadOnly, PropertyOrder(-5)]
        private List<IActor> currentPlan;

        [ShowInInspector, ReadOnly, PropertyOrder(-4)]
        private int actionIndex;

        private void Awake()
        {
            this.ConstuctActions();
            this.ConstructPlanner();
        }

        public void Play()
        {
            this.worldState.UpdateFacts();

            var goal = this.goals
                .Where(it => it.IsValid())
                .OrderByDescending(it => it.EvaluatePriority())
                .FirstOrDefault();

            if (goal == null)
            {
                Debug.LogWarning("Can't play: no valid goals!");
                return;
            }

            var actions = this.actions
                .Where(it => it.IsValid())
                .ToArray<IActor>();

            if (actions.Length <= 0)
            {
                Debug.LogWarning("Can't play: no valid actions!");
                return;
            }

            if (!this.planner.MakePlan(this.worldState, goal.ResultState, actions, out var plan))
            {
                Debug.LogWarning($"Can't make a plan for goal {goal.name}!");
                return;
            }
            
            if (plan.Count <= 0)
            {
                Debug.LogWarning($"Plan for goal {goal.name} is empty!");
                return;
            }
            
            this.currentGoal = goal;
            this.currentPlan = plan;

            this.actionIndex = 0;
            this.OnStarted?.Invoke();
            
            this.currentPlan[this.actionIndex].Play(callback: this);
        }

        void IActor.Callback.Invoke(IActor action, bool success)
        {
            if (!success)
            {
                this.Fail();
                return;
            }

            var planCompleted = this.actionIndex + 1 >= this.currentPlan.Count;
            if (planCompleted)
            {
                this.Complete();
                return;
            }

            this.actionIndex++;
            this.StartCoroutine(this.PlayNextAction());
        }
        
        public void Cancel()
        {
            if (this.currentPlan != null)
            {
                this.currentPlan[this.actionIndex].Cancel();
            }

            this.currentPlan = null;
            this.actionIndex = 0;
            this.OnCancel();
        }

        public void Replay()
        {
            this.Cancel();
            this.Play();
        }

        public bool TryPlay()
        {
            if (!this.IsPlaying)
            {
                this.Play();
                return true;
            }

            return false;
        }
        
        public void SynchronizeGoal()
        {
            var actualGoal = this.goals
                .Where(it => it.IsValid())
                .OrderByDescending(it => it.EvaluatePriority())
                .FirstOrDefault();

            if (actualGoal == null)
            {
                this.Cancel();
            }
            else if (!actualGoal.Equals(this.currentGoal))
            {
                this.Replay();
            }
        }

        private IEnumerator PlayNextAction()
        {
            yield return new WaitForFixedUpdate();
            this.currentPlan[this.actionIndex].Play(callback: this);
        }

        private void Fail()
        {
            this.currentPlan = null;
            this.actionIndex = 0;
            this.OnFail();
        }

        private void Complete()
        {
            this.currentPlan = null;
            this.actionIndex = 0;
            this.OnComplete();
        }

        protected virtual void OnFail()
        {
            this.OnFailed?.Invoke();
        }

        protected virtual void OnComplete()
        {
            this.OnCompleted?.Invoke();
        }

        protected virtual void OnCancel()
        {
            this.OnCanceled?.Invoke();
        }

        private void ConstuctActions()
        {
            foreach (var action in this.actions)
            {
                action.Construct(this.worldState);
            }
        }

        private void ConstructPlanner()
        {
            this.planner = PlannerFactory.CreatePlanner(this.plannerMode, this.actions);
        }
    }
}
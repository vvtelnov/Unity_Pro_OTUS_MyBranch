using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI.GOAP
{
    [AddComponentMenu("AI/GOAP/World State Looper")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(WorldState))]
    public sealed class WorldStateLooper : MonoBehaviour
    {
        [Space]
        [SerializeField]
        private bool playOnStart = true;

        [Space]
        [SerializeField]
        private float minUpdatePeriod = 0.1f;

        [SerializeField]
        private float maxUpdatePeriod = 0.2f;

        private WorldState worldState;
        
        private Coroutine coroutine;

        private void Awake()
        {
            this.worldState = this.GetComponent<WorldState>();
        }

        private void Start()
        {
            if (this.playOnStart)
            {
                this.Play();
            }
        }

        public void Play()
        {
            if (this.coroutine == null)
            {
                this.coroutine = this.StartCoroutine(this.UpdateLoop());
            }
        }

        public void Stop()
        {
            if (this.coroutine != null)
            {
                this.StopCoroutine(this.coroutine);
                this.coroutine = null;
            }
        }

        private IEnumerator UpdateLoop()
        {
            while (true)
            {
                var period = Random.Range(this.minUpdatePeriod, this.maxUpdatePeriod);
                yield return new WaitForSeconds(period);
                this.worldState.UpdateFacts();
            }
        }
    }
}
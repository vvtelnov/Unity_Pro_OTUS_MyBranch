using UnityEngine;
using UnityEngine.AI;

namespace AI.Tasks
{
    public abstract class Task_MoveByNavMesh : Task, ITaskCallback
    {
        protected abstract Task_MoveByPoints<Vector3> MoveTask { get; }

        private readonly NavMeshPath currentPath = new();

        private Vector3 targetPosition;

        private int navMeshAreas;

        public void SetNavMeshAreas(int navMeshAreas)
        {
            this.navMeshAreas = navMeshAreas;
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        protected override void Do()
        {
            var sourcePosition = this.EvaluateStartPosition();
            if (NavMesh.CalculatePath(sourcePosition, this.targetPosition, this.navMeshAreas, this.currentPath))
            {
                this.MoveTask.SetPath(this.currentPath.corners);
                this.MoveTask.Do(callback: this);
            }
            else
            {
                Debug.LogWarning($"Can not calculate path to {this.targetPosition}");
                this.Return(false);
            }
        }

        protected override void OnCancel()
        {
            this.MoveTask.Cancel();
        }

        protected abstract Vector3 EvaluateStartPosition();

        void ITaskCallback.Invoke(ITask task, bool success)
        {
            this.Return(success);
        }
    }
}
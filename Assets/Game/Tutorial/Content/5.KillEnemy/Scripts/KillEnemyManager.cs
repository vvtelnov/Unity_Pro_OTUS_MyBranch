using System;
using System.Collections;
using Entities;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class KillEnemyManager
    {
        private GameContext gameContext;
        
        [SerializeField]
        private MonoEntity enemyPrefab;
        
        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Transform spawnPoint;
        
        [SerializeField]
        private float destroyDelay = 1.25f;

        public void Construct(GameContext context)
        {
            this.gameContext = context;
        }

        public MonoEntity SpawnEnemy()
        {
            var enemy = GameObject.Instantiate(
                this.enemyPrefab,
                this.spawnPoint.position,
                this.spawnPoint.rotation,
                this.worldTransform
            );

            var gameElement = enemy.GetComponent<IGameElement>();
            this.gameContext.RegisterElement(gameElement);

            return enemy;
        }

        public IEnumerator DestroyEnemy(MonoEntity entity)
        {
            var gameElement = entity.GetComponent<IGameElement>();
            this.gameContext.UnregisterElement(gameElement);
            
            yield return new WaitForSecondsRealtime(this.destroyDelay);
            GameObject.Destroy(entity.gameObject);
        }
    }
}
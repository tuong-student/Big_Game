using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.System.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [Serializable]
        private struct EnemySpawnAndNumber
        {
            public GameObject enemyPref;
            public int number;
        }

        [SerializeField] List<EnemySpawnAndNumber> enemySpawnAndNumberList = new List<EnemySpawnAndNumber>();
        private float minX, maxX, minY, maxY;

        private void Start()
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;

            minX = collider.bounds.min.x;
            minY = collider.bounds.min.y;

            maxX = collider.bounds.max.x;
            maxY = collider.bounds.max.y;

            SpawnEnemies();
        }

        private void Update()
        {
        }

        public void SpawnEnemies()
        {
            foreach(EnemySpawnAndNumber enemySpawnAndNumber in enemySpawnAndNumberList)
            {
                for(int i = 0; i < enemySpawnAndNumber.number; i++)
                {
                    GameObject enemy = Instantiate(enemySpawnAndNumber.enemyPref, this.transform);
                    enemy.transform.position = GetRandomPosition();
                }
            }
        }

        public Vector2 GetRandomPosition()
        {
            float randX = UnityEngine.Random.Range(minX, maxX);
            float randY = UnityEngine.Random.Range(minY, maxY);

            return new Vector2(randX, randY);
        }
    }
}

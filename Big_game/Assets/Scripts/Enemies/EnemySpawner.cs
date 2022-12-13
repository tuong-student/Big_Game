using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] float minX, maxX, minY, maxY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D collider = GetComponent<Collider2D>();
            minX = collider.bounds.min.x;
            minY = collider.bounds.min.y;

            maxX = collider.bounds.max.x;
            maxY = collider.bounds.max.y;

            SpawnEnemies(4);
	    }   
    }

    public void SpawnEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyList[Random.Range(0, enemyList.Count)], this.gameObject.transform.parent);
            enemy.transform.position = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        return new Vector2(randX, randY);
    }
}

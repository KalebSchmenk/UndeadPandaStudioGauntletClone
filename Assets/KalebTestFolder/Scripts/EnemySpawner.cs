using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawnLocal;
    [SerializeField] int health = 2;

    private Transform spawnAt;
    private bool spawningEnemies = false;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnAt = spawnLocal.transform; 
    }

    private void StartSpawning(GameObject enemy)
    {
        StartCoroutine(IESpawningEnemy());
    }


    IEnumerator IESpawningEnemy()
    {
        spawningEnemies = true;

        Instantiate(enemy, spawnAt.position, Quaternion.identity);

        yield return new WaitForSeconds(7);

        StartCoroutine(IESpawningEnemy());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!spawningEnemies) StartSpawning(enemy);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0) Destroy(this.gameObject);
        }
    }
}

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

    private void StartSpawning(GameObject chaseTarget)
    {
        StartCoroutine(IESpawningEnemy(chaseTarget));
    }


    IEnumerator IESpawningEnemy(GameObject target)
    {
        spawningEnemies = true;

        var tempObj = Instantiate(enemy, spawnAt.position, Quaternion.identity);

        tempObj.GetComponent<EnemyAIController>().SetTarget(target);

        yield return new WaitForSeconds(7);

        StartCoroutine(IESpawningEnemy(target));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!spawningEnemies) StartSpawning(other.gameObject);
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

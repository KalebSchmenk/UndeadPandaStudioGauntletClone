using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject spawnLocal;
    [SerializeField] int spawnerHealth = 2;
    [SerializeField] float spawnEnemiesEvery = 7.0f;
    [SerializeField] int scoreOnKill = 25;
    [SerializeField] int maxSpawn = 10;

    [SerializeField] AudioClip soundOnDeath;
    [SerializeField] AudioClip soundOnDamage;

    private Transform spawnAt;
    private bool spawningEnemies = false;
    private ObjectSoundController soundController;

    // Start is called before the first frame update
    void Start()
    {
        spawnAt = spawnLocal.transform;

        soundController = GetComponent<ObjectSoundController>();
    }

    // Coroutine that spawns the given enemy prefab at the given interval
    IEnumerator IESpawningEnemy(GameObject target)
    {
        if (maxSpawn <= 0) yield break;

        maxSpawn--;

        spawningEnemies = true;

        var tempObj = Instantiate(enemy, spawnAt.position, Quaternion.identity);

        tempObj.GetComponent<EnemyAIController>().SetTarget(target);

        yield return new WaitForSeconds(spawnEnemiesEvery);

        StartCoroutine(IESpawningEnemy(target));

    }

    // Player "nearby" sphere trigger detector that starts the spawning coroutine 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(!spawningEnemies) StartCoroutine(IESpawningEnemy(other.gameObject));
        }
    }

    // Health damage system
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            spawnerHealth--;

            soundController.PlayAudio(soundOnDamage);

            Debug.Log("An enemy spawner has been shot dealing 1 damage leaving the spawner at " + spawnerHealth + " hp");

            if (spawnerHealth <= 0)
            {
                ScoreManager.AddScore(scoreOnKill);

                soundController.PlayAudio(soundOnDeath);

                Debug.Log("An enemy spawner has been shot dealing 1 damage and destroying it");
                Destroy(this.gameObject);
            }
        }
    }
}

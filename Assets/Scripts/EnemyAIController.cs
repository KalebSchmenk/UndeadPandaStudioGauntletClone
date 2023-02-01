using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    [SerializeField] int damageOutput = 1;
    [SerializeField] int enemyHealth = 1;
    [SerializeField] int scoreOnKill = 10;

    [SerializeField] AudioClip soundOnDeath;
    [SerializeField] AudioClip soundOnDamage;

    private GameObject player;
    private NavMeshAgent enemyNavMeshAgent;
    private ObjectSoundController soundController;


    // Start is called before the first frame update
    void Start()
    {
        // This could have been passed through when instantiated via a function
        // but I don't want to waste time with this time-sensitive project
        player = GameObject.FindGameObjectWithTag("Player");
        

        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

        soundController = GetComponent<ObjectSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyNavMeshAgent.SetDestination(player.transform.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().HurtPlayer(damageOutput);

            enemyHealth -= 1;

            soundController.PlayAudio(soundOnDamage);

            Debug.Log("An enemy has hit the player. It has done " + damageOutput + " damage to the player and has received 1 damage in return.");
            Debug.Log("This enemy is at " + enemyHealth + " hp.");

            if (enemyHealth <= 0)
            {
                Debug.Log("Enemy died");
                Destroy(this.gameObject);
            }

        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            enemyHealth -= 1;

            soundController.PlayAudio(soundOnDamage);

            Debug.Log("An enemy has been shot dealing 1 damage leaving the enemy at " + enemyHealth + " hp");

            if (enemyHealth <= 0)
            {
                ScoreManager.AddScore(scoreOnKill);

                soundController.PlayAudio(soundOnDeath);

                Debug.Log("Enemy died");
                Destroy(this.gameObject);
            }
        }
    }

    // Future proofing with the ability to dynamically set a target
    public void SetTarget(GameObject target)
    {
        player = target;
    }
}

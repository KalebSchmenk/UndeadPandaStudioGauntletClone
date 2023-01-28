using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    private GameObject player;

    private NavMeshAgent enemyNavMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        // This could have been passed through when instantiated via a function
        // but I don't want to waste time with this time-sensitive project
        player = GameObject.FindGameObjectWithTag("Player");
        

        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyNavMeshAgent.SetDestination(player.transform.position);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
        }
    }

    // Future proofing with the ability to dynamically set a target
    public void SetTarget(GameObject target)
    {
        player = target;
    }
}

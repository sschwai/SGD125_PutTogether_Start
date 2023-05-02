//using System; //commented out so that Random is specified for UnityEngine and no error
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    int currentNode;
    int previousNode;
    
    //State Machine
    public enum EnemyState
    {
        patrol,
        chase
    }
    EnemyState enemyState = EnemyState.patrol; //default state

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentNode = Random.Range(0,GameManager.gm.nodes.Length);
        previousNode = currentNode;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        { 
            case EnemyState.patrol: Patrol(); break;
            case EnemyState.chase: Chase(); break;
            default: break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            currentNode = Random.Range(0, GameManager.gm.nodes.Length);
            while (currentNode == previousNode)
            {
                Debug.Log("Repeated Node!");
                currentNode = UnityEngine.Random.Range(0, GameManager.gm.nodes.Length);
            }
            previousNode = currentNode;
        }

        if (other.tag == "Player")
        {
            enemyState = EnemyState.chase;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            currentNode = previousNode;
        }

        if (other.tag == "Player")
        {
            enemyState = EnemyState.patrol;
        }
    }

    void Patrol()
    {
        agent.destination = GameManager.gm.nodes[currentNode].position;
    }

    void Chase()
    {
        agent.destination = GameManager.gm.player.transform.position;
    }
}

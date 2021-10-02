using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public bool isNav, hasEgg;
    public string enemyName;
    public float eggRange;
    Rigidbody rb;
    GameObject holdingEgg;
    bool playerInEggRange, playerInSpawnRange;
    public LayerMask eggLayer, spawnLayer;
    Transform enemy;
    public GameObject [] allEggs, allSpawns;

    public void Awake() {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        navMeshAgent.enabled = true;
        hasEgg = false;
        isNav = true;

        // On awake capture all the eggs and spawns in the scene.
        allEggs = GameObject.FindGameObjectsWithTag("Egg"); 
        allSpawns = GameObject.FindGameObjectsWithTag("Spawn"); 

    }

    private void Update() {

        if (hasEgg) {
            EnemyReturn();
        }
        if(!hasEgg && isNav){
            navMeshAgent.enabled = true;
            FindClosestEgg();
        }
        if (!isNav) {
            navMeshAgent.enabled = false;
        }
        
    }
    public void FindClosestEgg() //Can most likely combine this and spawn navmesh scripts
    {
        float distanceToClosestEgg = Mathf.Infinity;
        GameObject closestEgg = null;

        foreach (GameObject currentEgg in allEggs)
        {
            float distanceToEgg = (currentEgg.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEgg < distanceToClosestEgg)
            {
                distanceToClosestEgg = distanceToEgg;
                closestEgg = currentEgg;
                closestEgg.layer = LayerMask.NameToLayer("CloseEgg");
            }
        }
        Debug.DrawLine (this.transform.position, closestEgg.transform.position);
        navMeshAgent.SetDestination(closestEgg.transform.position);
        playerInEggRange = Physics.CheckSphere(transform.position, eggRange, eggLayer);

        if (playerInEggRange)
        {
            closestEgg.GetComponent<EggManager>().EnemyPickUp(enemy);

            holdingEgg = closestEgg;

            hasEgg = true;
        }
    }

    public void EnemyReturn() {
        float distanceToClosestSpawn = Mathf.Infinity;
        GameObject closestSpawn = null;

        foreach (GameObject currentSpawn in allSpawns)
        {
            float distanceToEgg = (currentSpawn.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEgg < distanceToClosestSpawn)
            {
                distanceToClosestSpawn = distanceToEgg;
                closestSpawn = currentSpawn;
                closestSpawn.layer = LayerMask.NameToLayer("CloseSpawn");
            }
        }
        Debug.DrawLine (this.transform.position, closestSpawn.transform.position);
        navMeshAgent.SetDestination(closestSpawn.transform.position);
        playerInSpawnRange = Physics.CheckSphere(transform.position, eggRange, spawnLayer);

        if (playerInSpawnRange) //Need to figure this out
        {
            holdingEgg.GetComponent<EggManager>().isSaved = false;
            Debug.Log("Player stole the egg");
        }
    }

    public void DropEgg() {
        hasEgg = false;
        
        holdingEgg.transform.parent = null;
        holdingEgg.GetComponent<EggManager>().isTaken = false;
        holdingEgg.GetComponent<EggManager>().EnemyDrop();

    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EggManager : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Collider col;
    [SerializeField] private Transform movePositionTransform;
    public Vector3 puloc;
    public float hatchTime;
    public bool isHatched, isSaved, isTaken;
    public GameObject eggShell;
    bool isIncubated;
    float incubatedTime;
    Rigidbody rb;

    private void Awake() { 
        incubatedTime = 0f;
        navMeshAgent = GetComponent<NavMeshAgent>();
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        navMeshAgent.enabled = false;
        isTaken = false;
    }

    private void Update() {  
        if (isHatched && !isTaken) {
            navMeshAgent.enabled = true;
            navMeshAgent.destination = movePositionTransform.position;
        }
        if (isTaken) {
            navMeshAgent.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player") {
            ++incubatedTime;
            //Debug.Log(incubatedTime);
            if (incubatedTime >= hatchTime) {
                isHatched = true;
                col.isTrigger = false;
                navMeshAgent.enabled = true;
                ShellHatch();
            }
        }
    }

    public void EnemyPickUp (Transform enemy) {
        isTaken = true;
        transform.parent = enemy.transform;
        transform.localPosition = puloc;

        Transform eggObject =  GetComponent<Transform>();
        eggObject.localPosition = puloc + new Vector3 (0f, .5f, .5f); 
    }

    public void EnemyDrop () {
        navMeshAgent.enabled = true;
    }

    private void ShellHatch() {
        Destroy(eggShell);
        //Can also add audio and maybe animation for egg hatch.
    }
}
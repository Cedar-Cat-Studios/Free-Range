using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Rigidbody rb;
    public float pushForce;
    public float waitSeconds;
    public void Awake() {
        GameObject gm = GetComponent<GameObject>();
        rb = GetComponent<Rigidbody>();
    }
    public void RunAway(Collider col) {
        gameObject.GetComponent<EnemyNavMesh>().isNav = false;

        rb.isKinematic = false;

        Vector3 moveDirection = rb.transform.position - col.transform.position;
        rb.AddForce( moveDirection.normalized * pushForce * Time.deltaTime);

        //rb.AddForce(-transform.forward * pushForce * Time.deltaTime); //Change this to instead push them in the direction they were attacked
        StartCoroutine(ExampleCoroutine());        
    }

    IEnumerator ExampleCoroutine()
    {
        if (gameObject.GetComponent<EnemyNavMesh>().hasEgg) {
            gameObject.GetComponent<EnemyNavMesh>().DropEgg();
        }
        
        yield return new WaitForSeconds(waitSeconds);

        rb.isKinematic = true;
        gameObject.GetComponent<EnemyNavMesh>().isNav = true;
    }
}

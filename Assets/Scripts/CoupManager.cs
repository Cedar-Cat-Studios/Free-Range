using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoupManager : MonoBehaviour
{
    public int savedEggCount = 0;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EggChild") {
            Debug.Log("Egg in coup");
            ++savedEggCount;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "EggChild") {
            --savedEggCount;
        }
    }
}

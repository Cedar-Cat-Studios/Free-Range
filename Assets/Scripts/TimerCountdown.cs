using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerCountdown : MonoBehaviour
{
    public GameObject textDisplay;
    public GameObject gameManager;
    public int secondsLeft = 30;
    public bool takingAway = false;

    void Start() {
        textDisplay.GetComponent<TMP_Text>().text = "" + secondsLeft;
    }

    void Update() {
        if (takingAway == false && secondsLeft > 0) {
            StartCoroutine(TimerTake());
        }
        if (secondsLeft <= 0) {
            gameManager.GetComponent<GameManager>().EndGame();
        }
    }

    IEnumerator TimerTake() {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<TMP_Text>().text = "" + secondsLeft;
        takingAway = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject textDisplay;
    public GameObject gameManager;
    public Canvas winCanvas;
    public Canvas loseCanvas;

    public void FinalScoreLose(int savedEggs, int allEggs) {
        loseCanvas.enabled = true;
        textDisplay.GetComponent<TMP_Text>().text = "You saved " + savedEggs + " eggs\nOut of a total " + allEggs +" eggs.";
    }

    public void FinalScoreWin(int savedEggs, int allEggs) {
        winCanvas.enabled = true;
        textDisplay.GetComponent<TMP_Text>().text = "You saved " + savedEggs + " eggs\nOut of a total " + allEggs +" eggs.";
    }
}

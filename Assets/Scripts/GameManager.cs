using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This script will provide details on the level being over and also producing the results of the final match
    //To Add:
        // Providing a game over message when all eggs are hatched and into the coup
        // Showing the results at the end of the match to show how many eggs were saved
    public GameObject [] allEggs;
    public GameObject coup;
    public Canvas gameOverScreen;
    public Canvas levelCompletedScreen;
    public int eggCount;
    public int savedEggScore;

    private void Awake() {
        Time.timeScale = 1;
    }
    
    void Start()
    {
        //Checks to see the number of eggs in the active scene.
        eggCount = 0;
        allEggs = GameObject.FindGameObjectsWithTag("Egg"); 
        foreach (GameObject egg in allEggs) {
           ++eggCount;
        }
        Debug.Log(eggCount);
    }

    public void EndGame() {
        int savedEggScore = coup.GetComponent<CoupManager>().savedEggCount;
        Debug.Log("The final score is: " + savedEggScore);

        Time.timeScale = 0;
        if (savedEggScore == eggCount) {
            levelCompletedScreen.GetComponent<GameOver>().FinalScoreWin(savedEggScore, eggCount);
        }
        else {
            gameOverScreen.GetComponent<GameOver>().FinalScoreLose(savedEggScore, eggCount);
        }
        
    }
}

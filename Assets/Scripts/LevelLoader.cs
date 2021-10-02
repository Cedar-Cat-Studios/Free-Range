using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public float transitionTime = 1f;
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu () {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame ()
    {
        Debug.Log("Quit the Game.");
        Application.Quit();

    }
}

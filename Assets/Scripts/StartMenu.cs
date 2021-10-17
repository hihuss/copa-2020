using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayRoadtrip() 
    {
        Debug.Log("Clicked");
        int nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt(Constants.PRACTICE, 0);
        SceneManager.LoadScene(nextSceneToLoad);
    }

     public void ExitGame() {
        Debug.Log("Quitting the game...");
        Application.Quit();
        // UnityEditor.EditorApplication.isPlaying = false;
    }

    public void StartPractice(int lv) {
	PlayerPrefs.SetInt(Constants.PRACTICE, 1);
        SceneManager.LoadScene(lv);
    }
}

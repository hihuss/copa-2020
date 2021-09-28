using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelClass 
{
    public int Level { get; set; }
    public string Country { get; set; }
    public string Goal { get; set; }
    public string Details() 
    {
        return string.Format(" {0} is the level {1} and the goal is {2}", Country, Level, Goal);
    }
}

public class LoadingGame : MonoBehaviour
{
    public static int level;
    public GameObject gamePanel;
    public Text welcomeText;
    public Text explanationText;
    public Text timeTaken;
    public Button startLevelButton;
    public Button goToNextLevelButton;
    public Button startOverButton;
    public Button quitButton;

    private List<LevelClass> levelList = new List<LevelClass>() 
    {
        new LevelClass { Level = 1, Country = "Italy", Goal = "pizzas" },
        new LevelClass { Level = 2, Country = "the Netherlands", Goal = "tulips" },
        new LevelClass { Level = 3, Country = "Egypt", Goal = "vases" }
    };

    bool goalGateAlreadyDown = false;

    public float speed = 1;

    void Start()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(Constants.LEVEL, level);
        Debug.Log($"Level {level} loaded");

        SetLevelText();
    }

    void Update()
    {
        if(!goalGateAlreadyDown)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("Collectable");
            if(objs.Length == 0)
            {
                goalGateAlreadyDown = true;
                GameObject goalGate = GameObject.Find("GoalGate");
                GateScript script = goalGate.GetComponent<GateScript>();
                script.Shrink(3f);
            }
        }
    }

    private void SetLevelText() 
    {
        foreach (LevelClass lvl in levelList)
        {
            if (level == lvl.Level) 
            {
                welcomeText.text = $"Welcome to {lvl.Country}!";
                TextAllignemt(welcomeText);
                explanationText.text = $"Your goal is to collect all {lvl.Goal}, and get to the WHAT?";
                TextAllignemt(explanationText);
            }
        }
    }


    public void StartTheLevel() 
    {
        gamePanel.SetActive(false);
        explanationText.gameObject.SetActive(false);
        startLevelButton.gameObject.SetActive(false);

        GameObject playerObject = GameObject.Find("Player");
        PlayerController playerScript = playerObject.GetComponent<PlayerController>();
        playerScript.moveBall = true;
 
        Debug.Log($"Level {level} started!");
    }

    
    public void SetLevelCompletedCanvas() 
    {
        gamePanel.SetActive(true);
        goToNextLevelButton.gameObject.SetActive(true);
        timeTaken.gameObject.SetActive(true);

        welcomeText.text = $"Level {level} completed!";
        TextAllignemt(welcomeText);
        string timePlayerTook = PlayerPrefs.GetString(Constants.LEVEL + ' ' + level);
        timeTaken.text = $"Your time: {timePlayerTook}";
        TextAllignemt(timeTaken);
    }

    private void TextAllignemt(Text textElement) 
    {
        textElement.alignment = TextAnchor.MiddleCenter;
    }

    public void LoadNextLevel()
    {

        if (level < 3)
        {
            int nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneToLoad);
        }
        else {
            SetCanvasAfterLastLevel();
        }
    }

    private void SetCanvasAfterLastLevel() {
        goToNextLevelButton.gameObject.SetActive(false);
        startOverButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        welcomeText.text = "Good job! You finished the game.";
        TextAllignemt(welcomeText);
        string levelOneTime = PlayerPrefs.GetString("Level 1");
        string levelTwoTime = PlayerPrefs.GetString("Level 2");
        string levelThreeTime = PlayerPrefs.GetString("Level 3");

        string timeTakenString = $"Your times: ";
        if (!String.IsNullOrEmpty(levelOneTime)) {
            timeTakenString += $"\n 1. level: {levelOneTime}";
        }
        if (!String.IsNullOrEmpty(levelTwoTime)) {
            timeTakenString += $"\n 2. level: {levelTwoTime}";
        }
        if (!String.IsNullOrEmpty(levelThreeTime)) {
            timeTakenString += $"\n 3. level: {levelThreeTime}";
        }    
        timeTaken.text = timeTakenString;
    }

    public void StartOver() {
        SceneManager.LoadScene(0);
    }

    public void ExitGame() {
        Debug.Log("Quitting the game...");
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}



// possbily format the time the player took as:
//      TimeSpan time = TimeSpan.FromSeconds(currentTime);
//        string formatedCurrentTime = "";
//        if (time.Minutes != 0) 
//        {
//            formatedCurrentTime = string.Format("{0:0} minutes ", time.Minutes);
//        }
//        formatedCurrentTime = string.Format("{0:00} seconds and {1:00} milliseconds", time.Seconds, time.Milliseconds);
 
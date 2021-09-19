using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public static int level = 1;
    public GameObject gamePanel;
    public Text timerText;
    public Text welcomeText;
    public Text explanationText;

    List<LevelClass> levelList = new List<LevelClass>() 
    {
        new LevelClass { Level = 1, Country = "Italy", Goal = "pizzas" }
    };

    bool goalGateAlreadyDown = false;

    public float speed = 1;

    // Coroutine lastRoutine = null;


    void Start()
    {
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


    void SetLevelText() 
    {
        foreach (LevelClass lvl in levelList)
        {
            if (level == lvl.Level) 
            {
                welcomeText.text = $"Welcome to {lvl.Country}!";
                welcomeText.alignment = TextAnchor.MiddleCenter;
                explanationText.text = $"Your goal is to collect all {lvl.Goal}, and get to the WHAT?";
                explanationText.alignment = TextAnchor.MiddleCenter;
            }
        }
    }


    public void StartTheLevel() 
    {
        gamePanel.SetActive(false);
        GameObject playerObject = GameObject.Find("Player");
        PlayerController playerScript = playerObject.GetComponent<PlayerController>();
        playerScript.moveBall = true;

        Debug.Log($"Level {level} started!");
    }

    void MoveToNextScene() 
    {
        
    }

    void SetLevelCompletedCanvas() 
    {
        // level completed text: Level completed, size 50
    }



}

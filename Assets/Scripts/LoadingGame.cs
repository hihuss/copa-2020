using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{

    public static int level;
    bool goalGateAlreadyDown;
    Text timerText;
    float theTime;
    public float speed = 1;


    Coroutine lastRoutine = null;


    // Start is called before the first frame update
    void Start()
    {
        goalGateAlreadyDown = false;

    //    GameObject stage = GameObject.FindGameObjectWithTag("StageContainer");
    //    for (int count = 0; count < stage.transform.childCount; count++)
    //    {
    //        stage.transform.GetChild(count).gameObject.SetActive(false);
    //    }
    //    stage.transform.GetChild(level).gameObject.SetActive(true);

        level++;
       
        Debug.Log("Level 1 scene loaded");
    
        GameObject timerTextObj = GameObject.Find("TimerText");
        timerText = timerTextObj.GetComponent<Text>();
        timerText.color = Color.black;

        Invoke("StartTimer", 5);
    }

    // Update is called once per frame
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

    void StartTimer()
    {
        lastRoutine = StartCoroutine(TimerFunc());
    }

    void StopTimer()
    {
        if(lastRoutine == null)
            Debug.Log("Timer has not been started yet!");

        StopCoroutine(lastRoutine);
        lastRoutine = null;
    }

    IEnumerator TimerFunc()
    {   
        float t = 0f;
        while(true)
        {
            t += Time.deltaTime;
            string timeText = "Time: " + t.ToString("0.00");
            timerText.text = timeText;
            yield return null;
        }
    }
}

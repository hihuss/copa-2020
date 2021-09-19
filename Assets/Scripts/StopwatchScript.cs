using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StopwatchScript : MonoBehaviour
{

    public Text stopwatchText;

    bool stopwatchActive = false;
    float currentTime;

    void Start()
    {
        currentTime = 0;
    }

    void Update()
    {
        if (stopwatchActive == true) 
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        stopwatchText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopwatch ()
    {
        Debug.Log("Stopwatch started..");
        stopwatchActive = true;
    }

    public void StopStopwatch() 
    {
        stopwatchActive = false;
    }
}

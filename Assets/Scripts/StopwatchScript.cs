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
        stopwatchText.text = ConvertCurrentTimeToReadableStrinng();
    }

    public void StartStopwatch ()
    {
        Debug.Log("Stopwatch started..");
        stopwatchActive = true;
    }

    public void StopStopwatch() 
    {
        Debug.Log("Stopwatch stopped..");
        stopwatchActive = false;
        int level = PlayerPrefs.GetInt(Constants.LEVEL);
        PlayerPrefs.SetString(Constants.LEVEL + ' ' + level, ConvertCurrentTimeToReadableStrinng());
    }

    private string ConvertCurrentTimeToReadableStrinng()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
         return time.ToString(@"mm\:ss\:fff");
    } 
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StopwatchScript : MonoBehaviour
{
    public Text stopwatchText;

    bool stopwatchActive = false;
    float currentTime = 0;

    void Start()
    {
    }

    void Update()
    {
        if (stopwatchActive == true) 
        {
            currentTime = currentTime + Time.deltaTime;
        }
        stopwatchText.text = ConvertCurrentTimeToReadableString();
    }

    public void StartStopwatch ()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch() 
    {
        Debug.Log("Stopwatch stopped..");
        stopwatchActive = false;
        int level = PlayerPrefs.GetInt(Constants.LEVEL);
        PlayerPrefs.SetString(Constants.LEVEL + ' ' + level, ConvertCurrentTimeToReadableString());
    }

    private string ConvertCurrentTimeToReadableString()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
         return time.ToString(@"mm\:ss\:fff");
    } 
}

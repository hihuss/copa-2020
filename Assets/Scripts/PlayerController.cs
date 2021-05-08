using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    int level;

    Rigidbody rb;
    float speed = 5;

    GameObject loadingGame;

    public GameObject gameOverPanel;
    public Text levelCompletedTxt;
    public Text countDownText; 
    float timeLeft;

    bool paused = true; 

    GameObject[] collectables;
    
    // Start is called before the first frame update
    void Start()
    {
        level = LoadingGame.level;
        timeLeft = 5.0f;
        gameOverPanel.SetActive(true);
        levelCompletedTxt.gameObject.SetActive(false);
        countDownText.gameObject.SetActive(true);
        
        loadingGame = GameObject.FindWithTag("LoadingGameScript");
        Debug.Log(loadingGame);

        rb = GetComponent<Rigidbody>();

        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in collectables)
        {
            Debug.Log(collectable.name);
            collectable.SetActive(false);
        }
        collectables[0].SetActive(true);
        Debug.Log(collectables);

        StartCoroutine(DoPause());
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            countDownText.text = (timeLeft).ToString("0");
            if(timeLeft < 0)
            {
                countDownText.gameObject.SetActive(false);
                gameOverPanel.SetActive(false);
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        }
    }

    IEnumerator DoPause()
    {
        yield return new WaitForSeconds(5.0f);
        paused = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable")
        {
            int collectedIndex = Array.IndexOf(collectables, other.gameObject);
            if(collectedIndex < collectables.Length - 1)
            {
                collectables[collectedIndex + 1].SetActive(true);
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Goal")
        {
            // Level Completed
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
           levelCompletedTxt.text = $"Level {level} completed!";
           levelCompletedTxt.gameObject.SetActive(true);
           gameOverPanel.SetActive(true);
         //  loadingGame.SendMessage("StopTimer");
           Invoke("LoadNewLevel", 3.0f);
        }
    }


    void LoadNewLevel()
    {
     //   if (level == 1)
     //   {
            levelCompletedTxt.text = $"Congratulations! \r\nLevel 2 is coming soon!";
     //   } 
      //  else
      //  {
      //      gameOverPanel.SetActive(false);
      //      SceneManager.LoadScene("Level1");
      //  }
    }
}

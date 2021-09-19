using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    int level;

    Rigidbody rb;
    float speed = 5;

    public GameObject loadingGame;


   // public Text levelCompletedTxt;
 //   public Text countDownText;
 //   public Button startGameButton;
 //   public Text startGameButtonText;



    public bool moveBall = false;

    GameObject[] collectables;

    // Start is called before the first frame update
    void Start()
    {
 //      level = LoadingGame.level;
 //       gameOverPanel.SetActive(true);
  //      levelCompletedTxt.gameObject.SetActive(false);
       // countDownText.gameObject.SetActive(true);

   //     Debug.Log(loadingGame);

        rb = GetComponent<Rigidbody>();

        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in collectables)
        {
            collectable.SetActive(false);
        }
        collectables[0].SetActive(true);

     //   StartCoroutine(DoPause());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (moveBall)
        {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        }
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
 //          levelCompletedTxt.text = $"Level {level} completed!";
 //          levelCompletedTxt.gameObject.SetActive(true);
  //         gameOverPanel.SetActive(true);
         //  loadingGame.SendMessage("StopTimer");
           Invoke("LoadNewLevel", 3.0f);
        }
    }


    void LoadNewLevel()
    {
     //   if (level == 1)
     //   {
  //          levelCompletedTxt.text = $"Congratulations! \r\nLevel 2 is coming soon!";
     //   }
      //  else
      //  {
      //      gameOverPanel.SetActive(false);
      int nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
      SceneManager.LoadScene(nextSceneToLoad);
      //  }
    }
}

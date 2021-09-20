using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private LoadingGame loadingGameScript;
    private StopwatchScript stopwatchScript;

    public bool moveBall = false;

    private Rigidbody rb;
    private float speed = 5;
    private GameObject[] collectables;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in collectables)
        {
            collectable.SetActive(false);
        }
        collectables[0].SetActive(true);

        GameObject loadingGameGameObject = GameObject.Find("LoadingGameScript");
        loadingGameScript = loadingGameGameObject.GetComponent<LoadingGame>();

        
        GameObject canvasGameObject = GameObject.Find("Canvas");
        stopwatchScript = canvasGameObject.GetComponent<StopwatchScript>();
    }

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

        if (other.gameObject.tag == "Wall")
        {
            // count how many times the wall has been hit
        }

        if (other.gameObject.name == "Goal")
        {
            // Level Completed
            moveBall = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            stopwatchScript.StopStopwatch();
            loadingGameScript.SetLevelCompletedCanvas();
        }
    }
}

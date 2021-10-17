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
    private int timesHit = 0;

    private Rigidbody rb;
    private float speed = 5;
    private GameObject[] collectables;
    private int speedFactor;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedFactor = PlayerPrefs.GetInt(Constants.SPEED);
        speed = speed * (1 + (speedFactor - 1) * 0.2f);

        collectables = GameObject.FindGameObjectsWithTag("Collectable");
        foreach (GameObject collectable in collectables)
        {
            collectable.SetActive(false);
        }
        collectables[0].SetActive(true);

	SetParticles();

        GameObject loadingGameGameObject = GameObject.Find("LoadingGameScript");
        loadingGameScript = loadingGameGameObject.GetComponent<LoadingGame>();

        
        GameObject canvasGameObject = GameObject.Find("Canvas");
        stopwatchScript = canvasGameObject.GetComponent<StopwatchScript>();
    }

    private void FixedUpdate()
    {
        if (moveBall)
        {

          // float moveHorizontal = Input.GetAxis("Horizontal");
          // float moveVertical = Input.GetAxis("Vertical");

          // Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          // rb.AddForce(movement * speed);
           Vector3 tilt = Input.acceleration;
           tilt = Quaternion.Euler(90, 0, 0) * tilt;
           rb.AddForce(tilt * speed);
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
                SetParticles();
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "Goal")
        {
            // Level Completed
            moveBall = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

            stopwatchScript.StopStopwatch();
            loadingGameScript.SetLevelCompletedCanvas(timesHit);
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Wall hit!");
            timesHit++;
        }
    }

    private void SetParticles() 
    {
        GameObject[] glowings = GameObject.FindGameObjectsWithTag("Glowing");
        int particles = PlayerPrefs.GetInt(Constants.PARTICLES);
        Debug.Log("particles: " + particles);
        foreach (GameObject glowig in glowings)
        {
            glowig.SetActive(particles == 1);
        }
    }
}

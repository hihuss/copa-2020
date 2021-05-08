using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shrink(float time)
    {
        StartCoroutine(ShrinkTo(transform, new Vector3(transform.localScale.x, 0f, transform.localScale.z), time));
    }

    IEnumerator ShrinkTo(Transform transform, Vector3 scale, float timeToMove)
    {   
        Vector3 currentScale = transform.localScale;
        float t = 0f;
        while(t < 1)
        {
                t += Time.deltaTime / timeToMove;
                transform.localScale = Vector3.Lerp(currentScale, scale, t);
                yield return null;
        }

        gameObject.SetActive(false);
    }
}

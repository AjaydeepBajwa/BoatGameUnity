using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectRemover : MonoBehaviour
{
    bool circleCollided;
    bool circleAnimStarted;
    public GameObject circleMissedAnim;
    public GameObject rightCircleMissedAnim;
    public float delayTimer = 3f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;
        circleMissedAnim.SetActive(false);
        rightCircleMissedAnim.SetActive(false);
        circleCollided = false;
        circleAnimStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (circleCollided == true)
        {
            if (circleAnimStarted == false)
            {
                circleAnimStarted = true;
            }
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(2);
            }

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Obstacle"))
        {
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "Circle")
        {
            circleCollided = true;
            if (collision.gameObject.transform.position.x < 0)
            {
                circleMissedAnim.transform.position = collision.gameObject.transform.position;
                circleMissedAnim.SetActive(true);

            }
            else
            {
                rightCircleMissedAnim.SetActive(true);
                rightCircleMissedAnim.transform.position = collision.gameObject.transform.position;
            }

            Destroy(collision.gameObject);

        }
    }
    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(2);
    }


}

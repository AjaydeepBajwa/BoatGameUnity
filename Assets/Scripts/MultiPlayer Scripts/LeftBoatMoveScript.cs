using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class LeftBoatMoveScript : MonoBehaviourPun
{
    public int speed;
    private Vector2 direction;
    public float minimumX, maximumX;
    public Text scoreText1;
    //public string boatPosition;
    int score1 = 0;
    //int score2 = 0;

    //public UIManagerMulti uIManagerMulti;
    // Start is called before the first frame update
    void Start()
    {
            direction = Vector2.left;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            movePlayer();
        }


    }

    private void movePlayer()
    {
            if (Input.mousePosition.x <= Screen.width / 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("clicked");
                    //play boat move sound
                    //uiManager.playBoatMoveSound();
                    if (direction == Vector2.zero)
                    {
                        direction = Vector2.right;
                    }
                    else if (direction == Vector2.right)
                    {
                        direction = Vector2.left;
                    }
                    else if (direction == Vector2.left)
                    {
                        direction = Vector2.right;
                    }
                }
            }
            float moveAmount = speed * Time.deltaTime;
            transform.Translate(moveAmount * direction);

            //x coordinate for left boat should be -2.75 to -0.9
            Vector2 currentPosition = transform.position;
            //Using Clamp function to keep the boat between two lanes i.e. between minimumX to maximumX
            currentPosition.x = Mathf.Clamp(currentPosition.x, minimumX, maximumX);
            transform.position = currentPosition;
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Circle")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                score1++;
                scoreText1.text = "" + score1;
            }
            
            //score++;
            //points.text = ""+score;
            //Debug.Log(score + " points");
            //gotCircle = true;
            PhotonNetwork.Destroy(collision.gameObject);
            //points.text = "" + score;
            //gotCircle = false;
            //uIManagerMulti.addScore();
            //Destroy(dashAnimation);
            //dashAnimation.GetComponent<Animator>().enabled = false;
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            //destroyAnimation.SetActive(false);
            //if (dashActive == true)
            //{
            //    Destroy(collision.gameObject);
            //    destroyAnimation.transform.position = gameObject.transform.position;
            //    destroyAnimation.SetActive(true);
            //    uiManager.addScore();
            //    uiManager.addScore();
            //}
            //else if (dashActive == false)
            //{
            //    Invoke("EndTheGame", 2);
            //    //endGamee = true; 
            //    // dashAnimation.GetComponent<Animator>().enabled = false;
            //    Destroy(gameObject);
            //    destroyAnimation.transform.position = gameObject.transform.position;
            //    destroyAnimation.SetActive(true);

            //    //Destroy(dashAnimation.GetComponent<Animator>().;
            //    //Application.LoadLevel(1);
            //    SceneManager.LoadScene(1);

            //}
        }
    }
    public void EndTheGame()
    {
        //SceneManager.LoadScene(1);
    }
}

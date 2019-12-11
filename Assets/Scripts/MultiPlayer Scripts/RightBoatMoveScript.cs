using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RightBoatMoveScript : MonoBehaviourPun
{
    public int speed;
    private Vector2 direction;
    public float minimumX, maximumX;
    int score2;
    bool p2Dash;
    //public Text scoreText2;
    //public string boatPosition;
    //public UIManagerMulti uIManagerMulti;
    // Start is called before the first frame update
    void Start()
    {
            //boat is right
            direction = Vector2.right;
        p2Dash = false;

        Hashtable hash = new Hashtable();
        hash.Add("RightObstacleCollided", false);
        PhotonNetwork.SetPlayerCustomProperties(hash);

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
        if (Input.mousePosition.x >= Screen.width / 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("clicked");
                //playing boat move sound
                //uiManager.playBoatMoveSound();
                if (direction == Vector2.zero)
                {
                    direction = Vector2.left;
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

        Hashtable hash = new Hashtable();
        hash.Add("boat2Pos", transform.position.x);
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Circle")
        {
           if (!PhotonNetwork.IsMasterClient)
            {
                score2++;
                //scoreText2.text = "" + score2;

                Hashtable hash = new Hashtable();
                hash.Add("score", +score2);
                PhotonNetwork.SetPlayerCustomProperties(hash);
            }
          
            //score++;
            //points.text = ""+score;
            //Debug.Log(score + " points");
            //gotCircle = true;
            PhotonNetwork.Destroy(collision.gameObject);
            //points.text = "" + score;
            //gotCircle = false;
            //uIManagerMulti.addScore2();
            //Destroy(dashAnimation);
            //dashAnimation.GetComponent<Animator>().enabled = false;
        }
        if (collision.gameObject.tag == "Obstacle")
        {

            if (!PhotonNetwork.IsMasterClient)
            {
                Player p2 = PhotonNetwork.PlayerList[1];
                p2Dash = (bool)p2.CustomProperties["boat2Dash"];
                //destroyAnimation.SetActive(false);
                if (p2Dash == true)
                {
                    PhotonNetwork.Destroy(collision.gameObject);
                    //destroyAnimation.transform.position = gameObject.transform.position;
                    //destroyAnimation.SetActive(true);
                    score2++;
                    score2++;
                }

                else if (p2Dash == false)
                {
                    if(score2 >= 2)
                    {
                        score2 = score2 - 2;
                    }
                    else
                    {
                        score2 = 0;
                    }
                    
                    PhotonNetwork.Destroy(collision.gameObject);
                    //PhotonNetwork.Destroy
                }
                Hashtable hash = new Hashtable();
                hash.Add("score", +score2);
                hash.Add("RightObstacleCollided", true);
                hash.Add("RightObstacleCollidePos", collision.gameObject.transform.position.x);
                PhotonNetwork.SetPlayerCustomProperties(hash);
            }
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

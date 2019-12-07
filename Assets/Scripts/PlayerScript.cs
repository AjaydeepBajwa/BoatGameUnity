using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int speed;
    private Vector2 direction;
    public float minimumX, maximumX;
    public string boatPosition;
    //bool gotCircle;
    //public int score;
    //public Text points;
    public bool dashActive;

    public UIManager uiManager;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //uiManager = GetComponent<UIManager>();
        
        if (boatPosition == "left")
        {
            //boat is left
            direction = Vector2.left;
        }
        else
        {
            //boat is right
            direction = Vector2.right;
        }
        //direction = Vector2.zero;
    }

    public void setDashActive()
    {
        dashActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boatPosition == "left")
        {
            if (Input.mousePosition.x <= Screen.width / 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("clicked");
                    //play boat move sound
                    uiManager.playBoatMoveSound();
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
        else
        {
            if (Input.mousePosition.x >= Screen.width / 2)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("clicked");
                    //playing boat move sound
                    uiManager.playBoatMoveSound();
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
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Circle")
        {
            //score++;
            //points.text = ""+score;
            //Debug.Log(score + " points");
            //gotCircle = true;
            Destroy(collision.gameObject);
            //points.text = "" + score;
            //gotCircle = false;
            uiManager.addScore();
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            if (dashActive == true)
            {
                
                Destroy(collision.gameObject);
            }
            else
            {
                Destroy(gameObject);

                //Application.LoadLevel(1);
                SceneManager.LoadScene(1);
            }
        }
    }
}

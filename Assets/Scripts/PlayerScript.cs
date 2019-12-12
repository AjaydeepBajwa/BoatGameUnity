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
    public Text MicTextBool;
    public bool dashActive;

    public UIManager uiManager;
    public MicInputCheck micInputCheck;
    public GameObject dashAnimation;
    public GameObject destroyAnimation;
    bool animInstanciated;
    public float delayTimer = 1f;
    float timer;
    public bool endGamee;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;
        animInstanciated = false;
        dashAnimation.SetActive(false);
        destroyAnimation.SetActive(false);

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
    }

    public void setDashActive()
    {
        dashActive = true;
    }

    public void setDashInactive()
    {
        dashActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        dashActive = micInputCheck.dashActive;
        Vector2 dashAnimPosition = new Vector2(transform.position.x, transform.position.y - 1.73f);
        dashAnimation.transform.position = dashAnimPosition;
        if (dashActive == true)
        {
            if (animInstanciated == false)
            {
                dashAnimation.SetActive(true);
                animInstanciated = true;
            }

        }
        if (dashActive == false)
        {
            if (animInstanciated == true)
            {
                dashAnimation.SetActive(false);
                animInstanciated = false;
            }
        }
        MicTextBool.gameObject.GetComponent<Text>().text = "" + micInputCheck.dashRemaining +" blows left";

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

        if (endGamee == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SceneManager.LoadScene(2);
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Circle")
        {
            Destroy(collision.gameObject);
            uiManager.addScore();
        
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            destroyAnimation.SetActive(false);
            if (dashActive == true)
            {
                Destroy(collision.gameObject);
                destroyAnimation.transform.position = gameObject.transform.position;
                destroyAnimation.SetActive(true);
                uiManager.addScore();
                uiManager.addScore();
            }
            else if (dashActive == false)
            {
                Invoke("EndTheGame", 2);
                Destroy(gameObject);
                destroyAnimation.transform.position = gameObject.transform.position;
                destroyAnimation.SetActive(true);

                SceneManager.LoadScene(2);

            }
        }
    }
    public void EndTheGame()
    {
        SceneManager.LoadScene(2);
    }
}

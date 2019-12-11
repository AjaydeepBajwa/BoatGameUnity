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
    //public GameObject leftBoat;
    //public GameObject rightBoat;
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
        //uiManager = GetComponent<UIManager>();
        timer = delayTimer;
        animInstanciated = false;
        dashAnimation.SetActive(false);
        destroyAnimation.SetActive(false);
        //endGamee = false;

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

    public void setDashInactive()
    {
        dashActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("DASH ANIMATION INSTRANCE ID:" +dashAnimation.GetInstanceID());
        dashActive = micInputCheck.dashActive;
        Vector2 dashAnimPosition = new Vector2(transform.position.x, transform.position.y - 1.73f);
        dashAnimation.transform.position = dashAnimPosition;
        if (dashActive == true)
        {
            if (animInstanciated == false)
            {
                dashAnimation.SetActive(true);
                //Instantiate(dashAnimation, transform.position, transform.rotation);
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

            //timer = delayTimer;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //circleMissedAnim.SetActive(false);
                //circleAnimStarted = false;
                SceneManager.LoadScene(2);
            }

        }
        //MicTextBool.GetComponent<Text>().text = "" +timer;

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
            //Destroy(dashAnimation);
            //dashAnimation.GetComponent<Animator>().enabled = false;
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
                //endGamee = true; 
                // dashAnimation.GetComponent<Animator>().enabled = false;
                Destroy(gameObject);
                destroyAnimation.transform.position = gameObject.transform.position;
                destroyAnimation.SetActive(true);

                //Destroy(dashAnimation.GetComponent<Animator>().;
                //Application.LoadLevel(1);
                SceneManager.LoadScene(2);

            }
        }
    }
    public void EndTheGame()
    {
        SceneManager.LoadScene(2);
    }
}

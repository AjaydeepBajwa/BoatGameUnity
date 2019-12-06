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
    bool gameOver;

    public UIManager uiManager;

    //Blowing Variables
    AudioClip microphoneInput;
    bool microphoneInitialized;
    public float sensitivity = 100;
    public bool flapped;

    private void Awake()
    {
        //init microphone input
        if (Microphone.devices.Length > 0)
        {
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
            microphoneInitialized = true;
            Debug.Log("Mic Initialized");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //uiManager = GetComponent<UIManager>();
        gameOver = false;
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

    public void blowCheck()
    {
        //get mic volume
        int dec = 128;
        float[] waveData = new float[dec];
        int micPosition = Microphone.GetPosition(null) - (dec + 1); // null means the first microphone
        microphoneInput.GetData(waveData, micPosition);

        // Getting a peak on the last 128 samples
        float levelMax = 0;
        for (int i = 0; i < dec; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
                Debug.Log("Noise Level: " +levelMax);
            }
        }
        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));
        

        if (level > sensitivity && !flapped)
        {
            Debug.Log("Blow Accepted");
            flapped = true;
            uiManager.buttons[0].gameObject.SetActive(true);
        }
        
        if (level < sensitivity && flapped)
        {
            flapped = false;
        }
            
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
        blowCheck();
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
            Destroy(gameObject);
            gameOver = true;
            //Application.LoadLevel(1);
            SceneManager.LoadScene(1);
        }
    }
}

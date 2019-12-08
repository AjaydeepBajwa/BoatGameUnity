using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MicInputCheck : MonoBehaviour
{
    public static float MicLoudness;

    private string _device;

    public Button buttons;
    public Text MicText;
    //public Text SPMicText;
    //public Text EndGameMicText;
    //public PlayerScript playerScript;
    public bool dashActive;
    public float delayTimer = 2f;
    float timer;
    bool dashTimer;
    public int dashRemaining;

    //mic initialization
    void InitMic()
    {
        if (_device == null)
        {
            _device = Microphone.devices[0];

            Debug.Log("Device is:"+_device);
        }
        _clipRecord = Microphone.Start(_device, true, 999, 44100);
 
        //playerScript.setDashActive();
        //Debug.Log("DASH CONDITION: " + playerScript.dashActive);
    }

    void StopMicrophone()
    {
        Microphone.End(_device);
        Debug.Log("Mic Stopped");
    }


    AudioClip _clipRecord;
    int _sampleWindow = 128;

    //get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(null) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax <= wavePeak)
            {
                levelMax = wavePeak;
                Debug.Log("LEVEL MAX: " + levelMax);
            }
        }
        return levelMax;
    }

    private void Start()
    {
        timer = delayTimer;
        dashRemaining = 5;
        //playerScript = GetComponent<PlayerScript>();
    }

    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = LevelMax();
        //Debug.Log("MIC LOUDNESS: " + MicLoudness);
        if (MicLoudness > 0.8)
        {
            //EndGameMicText.gameObject.GetComponent<Text>().text = ""+MicLoudness;
            MicText.gameObject.GetComponent<Text>().text = ""+MicLoudness;
            //SPMicText.gameObject.GetComponent<Text>().text = ""+MicLoudness; 
            //buttons.gameObject.SetActive(false);
            //playerScript.setDashActive();
            //Debug.Log("DASH CONDITION: "+playerScript.dashActive);
            if (dashActive == false)
            {
                dashActive = true;
                dashRemaining = dashRemaining - 1;
            }
            timer = delayTimer;
            dashTimer = true;
        }
        if (dashTimer == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                dashActive = false;
                timer = delayTimer;
                dashTimer = false;
                //dashRemaining = dashRemaining - 1;
            }
        }
        
        
        //else if (MicLoudness < 0.8)
        //{
        //    MicText.gameObject.GetComponent<Text>().text = "" + MicLoudness;
        //    //SPMicText.gameObject.GetComponent<Text>().text = "MMMMMM";
        //    //playerScript.setDashInactive();
        //    if (dashActive == true)
        //    {
        //        dashActive = false;
        //    }
        //}
    }

    bool _isInitialized;
    // start mic when scene starts
    void OnEnable()
    {
        InitMic();
        _isInitialized = true;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!_isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
                _isInitialized = true;
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");
            _isInitialized = false;

        }
    }
}
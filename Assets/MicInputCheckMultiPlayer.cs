using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MicInputCheckMultiPlayer : MonoBehaviour
{
    public static float MicLoudness;

    private string _device;

    //public Button buttons;
    //public Text MicText;
    //public Text SPMicText;
    //public Text EndGameMicText;
    //public PlayerScript playerScript;
    public bool dashActive1;
    public bool dashActive2;
    public float delayTimer = 4f;
    float timer1;
    float timer2;
    bool dashTimer1;
    bool dashTimer2;
    public int dashRemaining1;
    public int dashRemaining2;

    //mic initialization
    void InitMic()
    {
        if (_device == null)
        {
            _device = Microphone.devices[0];

            Debug.Log("Device is:" + _device);
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
        timer1 = delayTimer;
        timer2 = delayTimer;
        dashRemaining1 = 5;
        dashRemaining2 = 5;
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
            //MicText.gameObject.GetComponent<Text>().text = "" + MicLoudness;
            //SPMicText.gameObject.GetComponent<Text>().text = ""+MicLoudness; 
            //buttons.gameObject.SetActive(false);
            //playerScript.setDashActive();
            //Debug.Log("DASH CONDITION: "+playerScript.dashActive);
            if (PhotonNetwork.IsMasterClient)
            {
                if (dashActive1 == false)
                {
                    if (dashRemaining1 >= 1)
                    {
                        dashActive1 = true;
                        dashRemaining1 = dashRemaining1 - 1;

                        Hashtable hash = new Hashtable();
                        hash.Add("p1DashRem", dashRemaining1);
                        PhotonNetwork.SetPlayerCustomProperties(hash);

                    }

                }
                timer1 = delayTimer;
                dashTimer1 = true;
            }
            if (!PhotonNetwork.IsMasterClient)
            {
                if (dashActive2 == false)
                {
                    if (dashRemaining2 >= 1)
                    {
                        dashActive2 = true;
                        dashRemaining2 = dashRemaining2 - 1;

                        Hashtable hash = new Hashtable();
                        hash.Add("p2DashRem", dashRemaining2);
                        PhotonNetwork.SetPlayerCustomProperties(hash);
                    }

                }
                timer2 = delayTimer;
                dashTimer2 = true;
            }
        }

        if (dashTimer1 == true)
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
            {
                dashActive1 = false;
                timer1 = delayTimer;
                dashTimer1 = false;
                //dashRemaining = dashRemaining - 1;
            }
        }

        if (dashTimer2 == true)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                dashActive2 = false;
                timer2 = delayTimer;
                dashTimer2 = false;
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

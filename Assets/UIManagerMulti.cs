﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMulti : MonoBehaviour
{
    //public Button[] buttons;
    //public Text[] btnTexts;
    public int score = 0;
    public int score2 = 0;
    public Text txtScore;
    public Text txtScore2;
    bool gotCircle;
    //public Text micTextBool;

    public AudioManager audioMan;
    // Start is called before the first frame update
    void Start()
    {
        audioMan.bgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
    }

    public void addScore()
    {
        score++;
        txtScore.text = "" +score;
        Debug.Log("Score isss: " + score);
    }

    public void addScore2()
    {
        score2++;
        txtScore2.text = "" + score2;
    }

    //public void PlaySingle()
    //{
    //    Debug.Log("Loaded 1P Scene");
    //    // Application.LoadLevel("1PScene");
    //    SceneManager.LoadScene(0);
    //}

    //public void GotoMultiplayerMenu()
    //{
    //    //Application.LoadLevel("1PScene");
    //    SceneManager.LoadScene(3);
    //}

    //public void Pause()
    //{

    //    if (Time.timeScale == 1)
    //    {
    //        Time.timeScale = 0;
    //        Debug.Log("Paused Game");
    //        buttons[0].gameObject.SetActive(true);
    //        buttons[1].gameObject.SetActive(true);
    //        buttons[2].gameObject.SetActive(true);
    //    }
    //    else if (Time.timeScale == 0)
    //    {
    //        Time.timeScale = 1;
    //        Debug.Log("Resumed Game");
    //        buttons[0].gameObject.SetActive(false);
    //        buttons[1].gameObject.SetActive(false);
    //        buttons[2].gameObject.SetActive(false);
    //    }
    //}

    public void playBoatMoveSound()
    {
        audioMan.boatMoveSound.Play();
    }

    //public void musicOnOff()
    //{
    //    if (audioMan.bgMusic.isPlaying == true)
    //    {
    //        audioMan.bgMusic.Stop();
    //        btnTexts[1].gameObject.GetComponent<Text>().text = "Music";
    //    }
    //    else
    //    {
    //        audioMan.bgMusic.Play();
    //        btnTexts[1].gameObject.GetComponent<Text>().text = "No Music";
    //    }

    //}
    //public void soundOnOff()
    //{
    //    if (audioMan.boatMoveSound.isActiveAndEnabled == true)
    //    {
    //        audioMan.boatMoveSound.enabled = false;
    //        btnTexts[0].gameObject.GetComponent<Text>().text = "Sound";
    //    }
    //    else
    //    {
    //        audioMan.boatMoveSound.enabled = true;
    //        btnTexts[0].gameObject.GetComponent<Text>().text = "No Sound";
    //    }

    //}
    //public void Quit()
    //{
    //    Time.timeScale = 1;
    //    SceneManager.LoadScene(2);
    //}
}
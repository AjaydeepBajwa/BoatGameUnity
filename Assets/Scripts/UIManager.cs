using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button[] buttons;
    public Text[] btnTexts;
    public int score = 0;
    public Text txtScore;
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
        txtScore.text = "" + score +" Points";
    }
    public void reduceScore()
    {
        score = score - 2 ;
        txtScore.text = "" + score + " Points";
    }

    public void PlaySingle()
    {
        Debug.Log("Loaded 1P Scene");
        // Application.LoadLevel("1PScene");
        SceneManager.LoadScene(1);
    }
    public void GotoMultiplayerMenu()
    {
        SceneManager.LoadScene(6);
    }

    public void GotoTwoPlayerMenu()
    {
        //Application.LoadLevel("1PScene");
        SceneManager.LoadScene(3);
    }

    public void GotoTeamGameMenu()
    {
        SceneManager.LoadScene(7);
    }

    public void Pause()
    {

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Debug.Log("Paused Game");
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
            buttons[2].gameObject.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            Debug.Log("Resumed Game");
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
        }
    }

    public void playBoatMoveSound()
    {
        audioMan.boatMoveSound.Play();
    }

    public void musicOnOff()
    {
        if (audioMan.bgMusic.isPlaying == true)
        {
            audioMan.bgMusic.Stop();
            btnTexts[1].gameObject.GetComponent<Text>().text = "Music";
        }
        else
        {
            audioMan.bgMusic.Play();
            btnTexts[1].gameObject.GetComponent<Text>().text = "No Music";
        }

    }
    public void soundOnOff()
    {
        if (audioMan.boatMoveSound.isActiveAndEnabled == true)
        {
            audioMan.boatMoveSound.enabled = false;
            btnTexts[0].gameObject.GetComponent<Text>().text = "Sound";
        }
        else
        {
            audioMan.boatMoveSound.enabled = true;
            btnTexts[0].gameObject.GetComponent<Text>().text = "No Sound";
        }

    }
    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}

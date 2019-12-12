using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class TeamGameUIManager : MonoBehaviour
{
    public Button[] buttons;
    public Text[] btnTexts;
    public int score = 0;
    public Text txtScore;
    public Text t1ScoreText;
    public Text t2ScoreText;
    public Text timeRemText;


    //public Text micTextBool;

    public AudioManager audioMan;
    public float timeRem = 60;

    // Start is called before the first frame update
    void Start()
    {
        audioMan.bgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        getPlayersScores();

        timeRem -= Time.deltaTime;
        timeRemText.text = "" + (int)timeRem;
        if (timeRem <= 0)
        {
            PhotonNetwork.LoadLevel(9);
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
    }

    public void addScore()
    {
        score++;
        txtScore.text = "" + score + " Points";

        Hashtable hash = new Hashtable();
        hash.Add("score", score);
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }
    public void reduceScore()
    {
        if (score >= 2)
        {
            score = score - 2;
        }
        else
        {
            score = 0;
        }
        
        txtScore.text = "" + score + " Points";

        Hashtable hash = new Hashtable();
        hash.Add("score", score);
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public void getPlayersScores()
    {

        //playerList[0] is master client
        int t1P1Index = (int)PhotonNetwork.LocalPlayer.CustomProperties["t1P1Index"];
        int t1P2Index = (int)PhotonNetwork.LocalPlayer.CustomProperties["t1P2Index"];
        //Debug.Log("TEAM 1 INDEXES ARE: " + t1P1Index + " and " + t1P2Index);

        int t2P1Index = (int)PhotonNetwork.LocalPlayer.CustomProperties["t2P1Index"];
        int t2P2Index = (int)PhotonNetwork.LocalPlayer.CustomProperties["t2P2Index"];
        //Debug.Log("TEAM 2 INDEXES ARE: " + t2P1Index + " and " + t2P2Index);

        string t1P1Name = PhotonNetwork.PlayerList[t1P1Index].NickName;
        string t1P2Name = PhotonNetwork.PlayerList[t1P2Index].NickName;
        string t2P1Name = PhotonNetwork.PlayerList[t2P1Index].NickName;
        string t2P2Name = PhotonNetwork.PlayerList[t2P2Index].NickName;
        Debug.Log("NAMES:" + t1P1Name + "  " + t1P2Name + " " + t2P1Name + " " + t2P2Name);

        int t1P1Score = (int)PhotonNetwork.PlayerList[t1P1Index].CustomProperties["score"];
        int t1P2Score = (int)PhotonNetwork.PlayerList[t1P2Index].CustomProperties["score"];
        int t2P1Score = (int)PhotonNetwork.PlayerList[t2P1Index].CustomProperties["score"];
        int t2P2Score = (int)PhotonNetwork.PlayerList[t2P2Index].CustomProperties["score"];

        Debug.Log("T1P1 SCORE: " + t1P1Score +"T1P2 SCORE: "+t1P2Score +"T2P1 SCORE: "+t2P1Score +"T2P2 SCORE: "+t2P2Score);

        int team1Score = t1P1Score + t1P2Score;
        int team2Score = t2P1Score + t2P2Score;

        Debug.Log("Team1 Score:" + team1Score + "and Team2 Score: " + team2Score);

        t1ScoreText.text = "Team 1: " + team1Score;
        t2ScoreText.text = "Team 2: " + team2Score;

        Hashtable hash = new Hashtable();
        hash.Add("t1P1Name", t1P1Name);
        hash.Add("t1P2Name", t1P2Name);
        hash.Add("t2P1Name", t2P1Name);
        hash.Add("t2P2Name", t2P2Name);
        hash.Add("t1P1Score", t1P1Score);
        hash.Add("t1P2Score", t1P2Score);
        hash.Add("t2P1Score", t2P1Score);
        hash.Add("t2P2Score", t2P2Score);
        hash.Add("team1Score", team1Score);
        hash.Add("team2Score", team1Score);
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public void gotoTeamGameOver()
    {
        PhotonNetwork.LoadLevel(9);
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
        PhotonNetwork.LoadLevel(9);
        //SceneManager.LoadScene(9);
    }
}

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


    public TeamGameMenuScript teamGameMenuScript;

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
        score = score - 2;
        txtScore.text = "" + score + " Points";

        Hashtable hash = new Hashtable();
        hash.Add("score", score);
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public void getPlayersScores()
    {

        //playerList[0] is master client in our case
        int team1p1Index = (int)PhotonNetwork.PlayerList[0].CustomProperties["team1p1"];
        int team1p2Index = (int)PhotonNetwork.PlayerList[0].CustomProperties["team1p2"];

        int team2p1Index = (int)PhotonNetwork.PlayerList[0].CustomProperties["team2p1"];
        int team2p2Index = (int)PhotonNetwork.PlayerList[0].CustomProperties["team2p2"];

        string team1P1Name = (string)PhotonNetwork.PlayerList[team1p1Index].NickName;
        string team1P2Name = (string)PhotonNetwork.PlayerList[team1p2Index].NickName;
        string team2P1Name = (string)PhotonNetwork.PlayerList[team2p1Index].NickName;
        string team2P2Name = (string)PhotonNetwork.PlayerList[team2p2Index].NickName;

        int team1P1Score = (int)PhotonNetwork.PlayerList[team1p1Index].CustomProperties["score"];
        int team1P2Score = (int)PhotonNetwork.PlayerList[team1p2Index].CustomProperties["score"];
        int team2P1Score = (int)PhotonNetwork.PlayerList[team2p1Index].CustomProperties["score"];
        int team2P2Score = (int)PhotonNetwork.PlayerList[team2p2Index].CustomProperties["score"];

        int team1Score = team1P1Score + team1P2Score;
        int team2Score = team2P1Score + team2P2Score;

        t1ScoreText.text = "" + team1Score;
        t2ScoreText.text = "" + team2Score;

        Debug.Log("Team1 Score:" + team1Score + "and Team2 Score: " + team2Score);

        Hashtable hash = new Hashtable();
        hash.Add("team1P1Name", team1P1Name);
        hash.Add("team1P2Name", team1P2Name);
        hash.Add("team2P1Name", team2P1Name);
        hash.Add("team2P2Name", team2P2Name);

        hash.Add("team1P1Score", team1P1Score);
        hash.Add("team1P2Score", team1P2Score);
        hash.Add("team2P1Score", team2P1Score);
        hash.Add("team2P2Score", team2P2Score);

        hash.Add("team1Score", team1Score);
        hash.Add("team2Score", team1Score);
        PhotonNetwork.SetPlayerCustomProperties(hash);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class TeamGameOverScript : MonoBehaviour
{
    public Text team1ScoreText;
    public Text team2ScoreText;
    public Text t1p1NameText;
    public Text t1p2NameText;
    public Text t2p1NameText;
    public Text t2p2NameText;

    public Text t1p1ScoreText;
    public Text t1p2ScoreText;
    public Text t2p1ScoreText;
    public Text t2p2ScoreText;
    public Text winnerTeamNameText;

    public Button gotoHomeBtn;
    // Start is called before the first frame update
    void Start()
    {
        getScores();
    }

    public void getScores()
    {
        int team1Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["team1Score"];
        int team2Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["team2Score"];

        team1ScoreText.text = "Total: " + team1Score;
        team2ScoreText.text = "Total:" + team2Score;

        t1p1NameText.text = ""+(string)PhotonNetwork.PlayerList[0].CustomProperties["t1P1Name"]+":";
        t1p2NameText.text = ""+ (string)PhotonNetwork.PlayerList[0].CustomProperties["t1P2Name"] + ":";
        t2p1NameText.text = ""+ (string)PhotonNetwork.PlayerList[0].CustomProperties["t2P1Name"] + ":";
        t2p2NameText.text = ""+ (string)PhotonNetwork.PlayerList[0].CustomProperties["t2P2Name"] + ":";

        Debug.Log("At game over: Team1: " + team1Score + "team2: " + team2Score);


        int t1p1Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["t1P1Score"];
        int t1p2Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["t1P2Score"];
        int t2p1Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["t2P1Score"];
        int t2p2Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["t2P2Score"];

        t1p1ScoreText.text = ""+t1p1Score;
        t1p2ScoreText.text = "" + t1p2Score;
        t2p1ScoreText.text = "" + t2p1Score;
        t2p2ScoreText.text = "" + t2p2Score;


        if (team1Score > team2Score)
        {
            winnerTeamNameText.text = "Team 1 Wins";
        }
        if (team2Score > team1Score)
        {
            winnerTeamNameText.text = "Team 2 Wins";
        }
        if (team1Score == team2Score)
        {
            winnerTeamNameText.text = "It's a tie";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToHome()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
}

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
        int team1Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["team1Score"];
        int team2Score = (int)PhotonNetwork.PlayerList[0].CustomProperties["team2Score"];

        team1ScoreText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team1Score"];
        team2ScoreText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team1Score"];

        t1p1NameText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team1P1Score"];
        t1p2NameText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team1P2Score"];
        t2p1NameText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team2P1Score"];
        t2p2NameText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team2P2Score"];


        t1p1ScoreText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team1P1Score"];
        t1p2ScoreText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team1P2Score"];
        t2p1ScoreText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team2P1Score"];
        t2p2ScoreText.text = (string)PhotonNetwork.PlayerList[0].CustomProperties["team2P2Score"];

        if (team1Score > team2Score)
        {
            winnerTeamNameText.text = "Team 1 Wins";
        }
        else
        {
            winnerTeamNameText.text = "Team2 Wins";
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
        SceneManager.LoadScene(0);
    }
}

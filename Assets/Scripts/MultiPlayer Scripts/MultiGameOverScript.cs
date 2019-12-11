using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MultiGameOverScript : MonoBehaviour
{

    public Text p1ScoreText;
    public Text p2ScoreText;
    public Text p1NameText;
    public Text p2NameText;
    public Text winnerNameText;
    // Start is called before the first frame update
    void Start()
    {
        Player p1 = PhotonNetwork.PlayerList[0];
        Player p2 = PhotonNetwork.PlayerList[1];

        Debug.Log("Name:" + p1.NickName + "Score: " + p1.CustomProperties["score"] + "Name:" + p2.NickName + "Score: " + p2.CustomProperties["score"]);

        int p1Score = (int)p1.CustomProperties["score"];
        int p2Score = (int)p2.CustomProperties["score"];

        if (p1Score > p2Score)
        {
            winnerNameText.text = "" + p1.NickName +"WON";
        }
        else if (p2Score > p1Score)
        {
            winnerNameText.text = "" + p2.NickName +"WON";
        }
        else
        {
            winnerNameText.text = "IT'S A TIE";
        }

        p1NameText.text = "" + p1.NickName;
        p2NameText.text = "" + p2.NickName;
        p1ScoreText.text = "" + p1Score;
        p2ScoreText.text = "" + p2Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

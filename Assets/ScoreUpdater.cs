using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    //int score1;
    //int score2;
    public Text score1Text;
    public Text score2Text;
    public Text dashRem1Text;
    public Text dashRem2Text;
    public Text timeRemText;

    public float timeRem = 60;
    // Start is called before the first frame update
    void Start()
    {
        score1Text.text = "0";
        score2Text.text = "0";
        dashRem1Text.text = "5";
        dashRem2Text.text = "5";
    }

    // Update is called once per frame
    void Update()
    {

        timeRem -= Time.deltaTime;
        timeRemText.text = "" + timeRem;
        if (timeRem <= 0)
        {
            PhotonNetwork.LoadLevel(5);
        }
            //string scores = string.Empty;

            //foreach (Player p in PhotonNetwork.PlayerList)
            //{
            //    scores += p.NickName + " Score:" + p.CustomProperties["score"] +"\n";
            //}
        Player p1 = PhotonNetwork.PlayerList[0];
        Player p2 = PhotonNetwork.PlayerList[1];

        Debug.Log("Name:" + p1.NickName + "Score: " + p1.CustomProperties["score"] + "Name:" + p2.NickName + "Score: " + p2.CustomProperties["score"]);

        score1Text.text = "" + p1.CustomProperties["score"];
        score2Text.text = "" + p2.CustomProperties["score"];

        dashRem1Text.text = "" + p1.CustomProperties["p1DashRem"];
        dashRem2Text.text = "" + p2.CustomProperties["p2DashRem"];

        //Debug.Log(scores);
    }
}

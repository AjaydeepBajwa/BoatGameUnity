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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

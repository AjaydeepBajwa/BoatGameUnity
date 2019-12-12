using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class NameTag : MonoBehaviourPun
{
    public Text nameTag;
    //public Text nameTag2;
    // Start is called before the first frame update
    private void Start()
    {
     //if (photonView.IsMine)
     //   {
     //       return;
     //   }
        Debug.Log("NAME TAG SCRIPT SDTASRED");
        SetName();
    }

    private void SetName()
    {
        nameTag.text = photonView.Owner.NickName;
        Debug.Log("Name is "+nameTag.text);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

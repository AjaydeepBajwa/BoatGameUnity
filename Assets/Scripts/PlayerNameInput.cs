﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] public InputField playerNameField = null;
    [SerializeField] public Button goButton = null;

    private const string playerPrefsNameKey = "playerName";

    // Start is called before the first frame update
    void Start()
    {
        setUpInputField();
    }

    public void setUpInputField()
    {
        if (!PlayerPrefs.HasKey(playerPrefsNameKey))
        {
            return;
        }
        string defaultName = PlayerPrefs.GetString(playerPrefsNameKey);
        playerNameField.text = defaultName;

        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string defaultName)
    {
        goButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {

        string nameOfPlayer = playerNameField.text;
        PhotonNetwork.NickName = nameOfPlayer;

        PlayerPrefs.SetString(playerPrefsNameKey, nameOfPlayer);

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}

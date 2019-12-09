using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MultiMenuScene : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponentPanel = null;
    [SerializeField] private GameObject waitingStatusPanel = null;
    [SerializeField] private Text waitingStatusText = null;

    private bool isConnecting = false;

    private const string gameVersion = "0.1";
    private const int maxPlayersPerRoom = 2;

    private void Awake()
    {
        try
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            Debug.Log("Scene Synced");
        }
        catch
        {
            Debug.Log("Scene not synced");
        }
        
    }
    public void findOpponent()
    {
        isConnecting = true;

        findOpponentPanel.SetActive(false);
        waitingStatusPanel.SetActive(true);

        waitingStatusText.text = "Searching...";

        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("photon network is connected");
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("connecting using settings");
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingStatusPanel.SetActive(false);
        findOpponentPanel.SetActive(true);

        Debug.Log($"Disconnected due to {cause}");

        Debug.Log("Disconnected due to" +cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients are waitng for oppenent,creating a new room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client joinecd the room");
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount != maxPlayersPerRoom)
        {
            waitingStatusText.text = "Waiting for opponent";
            Debug.Log("Client is waiting for oppnent");
        }
        else
        {
            waitingStatusText.text = "Opponent found";
            Debug.Log("Match is ready to begin");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
        {

            PhotonNetwork.CurrentRoom.IsOpen = false;
            waitingStatusText.text = "Opponent found";
            Debug.Log("Match is ready to begin");

            PhotonNetwork.LoadLevel(4);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Multimenu scene loaded");   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

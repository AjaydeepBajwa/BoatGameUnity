using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TeamGameMenuScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findOpponentPanel = null;
    [SerializeField] private GameObject waitingStatusPanel = null;
    [SerializeField] private Text waitingStatusText = null;

    public Button P1Button;
    public Button P2Button;
    public Button P3Button;
    public Button P4Button;

    public Text P1NameText;
    public Text P2NameText;
    public Text P3NameText;
    public Text P4NameText;

    public Button startGameButton;
    Button[] playerButtons;
    Text[] playerNamesText;
    bool team2joined = false;
    int i = 0;

    private bool isConnecting = false;


    private const string gameVersion = "0.1";
    private const int maxPlayersPerRoom = 4;

    //Player[] team1players;
    //Player[] team2Players;
    List<Player> team1Players = new List<Player>();
    List<Player> team2Players = new List<Player>();

    List<int> team1PlayersIndexes = new List<int>();
    List<int> team2PlayersIndexes = new List<int>();


    private void Awake()
    {
        playerButtons = new Button[] { P1Button, P2Button, P3Button, P4Button};
        playerNamesText = new Text[] { P1NameText, P2NameText, P3NameText, P4NameText };

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

        Debug.Log("Disconnected due to" + cause);
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


        if (playerCount != maxPlayersPerRoom)
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

            PhotonNetwork.CurrentRoom.IsOpen = true;
            waitingStatusText.text = "Opponent found";
            Debug.Log("Match is ready to begin");

            int playersJoinedCount = PhotonNetwork.PlayerList.Length;

            for (int i = 0; i < playersJoinedCount; i++)
            {
                playerButtons[i].gameObject.SetActive(true);
                playerNamesText[i].text = PhotonNetwork.PlayerList[i].NickName;
                Debug.Log("Players Name: " + PhotonNetwork.PlayerList[i].NickName);
            }
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
        int playersJoinedCount = PhotonNetwork.PlayerList.Length;

        for (int i = 0; i < playersJoinedCount; i++)
        {
            playerButtons[i].gameObject.SetActive(true);
            playerNamesText[i].text = PhotonNetwork.PlayerList[i].NickName;
            //Debug.Log("Players Name: " + PhotonNetwork.PlayerList[i].NickName);
        }
        setTeams();
    }

    public void switchTeam()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            if (i % 2 == 0)
            {

                string buttonClickedName = EventSystem.current.currentSelectedGameObject.name;
                //Debug.Log("CURRENT BUTTON: " + buttonClickedName);
                //EventSystem.current.currentSelectedGameObject.SetActive(false);
                int selectedButtonTag = int.Parse(EventSystem.current.currentSelectedGameObject.tag);
                Button selectedButton = playerButtons[selectedButtonTag];
                selectedButton.transform.position = new Vector2(800, selectedButton.transform.position.y);

                team2Players.Add(PhotonNetwork.PlayerList[selectedButtonTag]);

            }
            if (i % 2 != 0)
            {
                string buttonClickedName = EventSystem.current.currentSelectedGameObject.name;
                //Debug.Log("CURRENT BUTTON: " + buttonClickedName);
                //EventSystem.current.currentSelectedGameObject.SetActive(false);
                int selectedButtonTag = int.Parse(EventSystem.current.currentSelectedGameObject.tag);
                Button selectedButton = playerButtons[selectedButtonTag];
                selectedButton.transform.position = new Vector2(300, selectedButton.transform.position.y);

                team1Players.Add(PhotonNetwork.PlayerList[selectedButtonTag]);
            }
            i++;

        }
    }

    public void setTeams()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersPerRoom)
            {

                for (int i = 0; i < playerButtons.Length; i++)
                {
                    if (playerButtons[i].transform.position.x < 500)
                    {
                        team1Players.Add(PhotonNetwork.PlayerList[i]);
                        team1PlayersIndexes.Add(i);
                        //Debug.Log("Team1 Players indexes:" + PhotonNetwork.PlayerList[i].NickName);
                    }
                    else
                    {
                        team2Players.Add(PhotonNetwork.PlayerList[i]);
                        team2PlayersIndexes.Add(i);
                        //Debug.Log("Team2 Players:" + PhotonNetwork.PlayerList[i].NickName);
                    }
                    if ((team1PlayersIndexes.Count == 2) && (team2PlayersIndexes.Count == 2))
                    {
                        Debug.Log("TEAM1 players: " + PhotonNetwork.PlayerList[team1PlayersIndexes[0]].NickName + "and " + PhotonNetwork.PlayerList[team1PlayersIndexes[1]].NickName);
                        Debug.Log("TEAM2 players: " + PhotonNetwork.PlayerList[team2PlayersIndexes[0]].NickName + "and " + PhotonNetwork.PlayerList[team1PlayersIndexes[2]].NickName);
                        //startTeamGame();
                    }
                }
                startGameButton.gameObject.SetActive(true);
            }
        }
    }

    public void startTeamGame()
    {
        
        PhotonNetwork.LoadLevel(8);
    }
 
}

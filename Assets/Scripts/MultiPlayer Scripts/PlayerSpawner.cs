using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private GameObject player2Prefab = null;

    public bool dashActive1;
    public bool dashActive2;
    bool anim1Instanciated;
    bool anim2Instanciated;
    public GameObject dashAnim1;
    public GameObject dashAnim2;
    public MicInputCheckMultiPlayer micInputCheckMulti;

    // Start is called before the first frame update
    void Start()
    {
        anim1Instanciated = false;
        anim2Instanciated = false;
        dashAnim1.SetActive(false);
        dashAnim2.SetActive(false);

        Hashtable hash = new Hashtable();
        hash.Add("boat1Dash", false);
        hash.Add("boat2Dash", false);
        PhotonNetwork.SetPlayerCustomProperties(hash);

        Vector3 boat1Position = new Vector3(-1.7f, -3.2f, 0);
        Vector3 boat2Position = new Vector3(1.7f, -3.2f, 0);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, boat1Position, Quaternion.identity);
        }
        else if (!PhotonNetwork.IsMasterClient)
        {
           PhotonNetwork.Instantiate(player2Prefab.name, boat2Position, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            dashActive1 = micInputCheckMulti.dashActive1;
            Player p1 = PhotonNetwork.PlayerList[0];
            float p1X = (float)p1.CustomProperties["boat1Pos"];
            Vector2 dashAnimPosition = new Vector2(p1X, -3.66f);
            dashAnim1.transform.position = dashAnimPosition;
            if (dashActive1 == true)
            {
                if (anim1Instanciated == false)
                {
                    dashAnim1.SetActive(true);
                    anim1Instanciated = true;

                    Hashtable hash = new Hashtable();
                    hash.Add("boat1Dash", anim1Instanciated);
                    PhotonNetwork.SetPlayerCustomProperties(hash);
                }

            }
            if (dashActive1 == false)
            {
                if (anim1Instanciated == true)
                {
                    dashAnim1.SetActive(false);
                    anim1Instanciated = false;

                    Hashtable hash = new Hashtable();
                    hash.Add("boat1Dash", anim1Instanciated);
                    PhotonNetwork.SetPlayerCustomProperties(hash);
                }
            }
        }

        if (!PhotonNetwork.IsMasterClient)
        {
            dashActive2 = micInputCheckMulti.dashActive2;

            Player p2 = PhotonNetwork.PlayerList[1];
            float p2X = (float)p2.CustomProperties["boat2Pos"];

            Vector2 dashAnimPos = new Vector2(p2X, -3.66f);
            dashAnim2.transform.position = dashAnimPos;
            if (dashActive2 == true)
            {
                if (anim2Instanciated == false)
                {
                    dashAnim2.SetActive(true);
                    anim2Instanciated = true;

                    Hashtable hash = new Hashtable();
                    hash.Add("boat2Dash", anim2Instanciated);
                    PhotonNetwork.SetPlayerCustomProperties(hash);
                }

            }
            if (dashActive2 == false)
            {
                if (anim2Instanciated == true)
                {
                    dashAnim2.SetActive(false);
                    anim2Instanciated = false;

                    Hashtable hash = new Hashtable();
                    hash.Add("boat2Dash", anim2Instanciated);
                    PhotonNetwork.SetPlayerCustomProperties(hash);
                }
            }
        }
        
    }
}

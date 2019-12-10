using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private GameObject player2Prefab = null;
    // Start is called before the first frame update
    void Start()
    {
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
        
    }
}

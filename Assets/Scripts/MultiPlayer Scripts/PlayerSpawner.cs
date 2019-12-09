using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 boatPosition = new Vector3(-2.75f, -4.87f, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, boatPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnObjectsOnRight : MonoBehaviourPun
{
    public GameObject circle2;
    public GameObject obstacle2;

    public float maxPos = 2.75f;
    public float delayTimer = 1f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                float[] XPosArray = new float[] { 0.6f, 1.7f };
                float randomPos = (XPosArray[Random.Range(0, XPosArray.Length)]);
                Vector2 objectPos = new Vector2(randomPos, transform.position.y);
                int randomObject = Random.Range(0, 2);
                if (randomObject == 0)
                {
                    PhotonNetwork.Instantiate(circle2.name, objectPos, transform.rotation);
                }
                else
                {
                    PhotonNetwork.Instantiate(obstacle2.name, objectPos, transform.rotation);
                }

                timer = delayTimer;
            }
        }

    }
}

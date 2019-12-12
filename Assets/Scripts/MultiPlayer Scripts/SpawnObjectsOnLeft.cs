using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnObjectsOnLeft : MonoBehaviourPun
{
    public GameObject circle;
    public GameObject obstacle;
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
        if (PhotonNetwork.IsMasterClient)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                float[] XPosArray = new float[] { -1.7f, -0.6f };
                float randomPos = (XPosArray[Random.Range(0, XPosArray.Length)]);
                Vector2 objectPos = new Vector2(randomPos, transform.position.y);
                int randomObject = Random.Range(0, 2);
                if (randomObject == 0)
                {
                    PhotonNetwork.Instantiate(circle.name, objectPos, transform.rotation);

                }
                else
                {
                    PhotonNetwork.Instantiate(obstacle.name, objectPos, transform.rotation);

                }

                timer = delayTimer;
            }
        }
        
    }
}

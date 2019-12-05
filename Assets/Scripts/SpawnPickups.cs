using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
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
        timer -= Time.deltaTime;
        if( timer <= 0)
        {
            float[] XPosArray = new float[] { -2.75f, -1, 1, 2.75f };
            float randomPos = (XPosArray[Random.Range(0, XPosArray.Length)]);
            Vector2 circlePos = new Vector2(randomPos, transform.position.y);
            //Vector3 circlePos = new Vector3(randomPos, transform.position.y, transform.position.z);
            //Vector3 circlePos = new Vector3(Random.Range(-2.75f, 2.75f), transform.position.y, transform.position.z);
            int randomObject = Random.Range(0, 2);
            if (randomObject == 0)
            {
                Instantiate(circle, circlePos, transform.rotation);
            }
            else
            {
                Instantiate(obstacle, circlePos, transform.rotation);
            }
            
            timer = delayTimer;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public GameObject circle;
    public GameObject obstacle;
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
        timer -= Time.deltaTime;
        if( timer <= 0)
        {
            float[] XPosArray = new float[] { -2.75f,-1,1,2.75f };
            //float[] LeftXPosArray = new float[] { -2.75f, -1};
            //float[] RightXPosArray = new float[] { 1, 2.75f};
            float randomPos = (XPosArray[Random.Range(0, XPosArray.Length)]);
            //float randomPosLeft = (LeftXPosArray[Random.Range(0, LeftXPosArray.Length)]);
            //float randomPosRight = (RightXPosArray[Random.Range(0, RightXPosArray.Length)]);
            Vector2 objectPos = new Vector2(randomPos, transform.position.y);
            //Vector2 objectPosLeft = new Vector2(randomPosLeft, transform.position.y);
            //Vector2 objectPosRight = new Vector2(randomPosRight, transform.position.y);
            //Vector3 circlePos = new Vector3(randomPos, transform.position.y, transform.position.z);
            //Vector3 circlePos = new Vector3(Random.Range(-2.75f, 2.75f), transform.position.y, transform.position.z);
            int randomObject = Random.Range(0, 2);
            if (randomObject == 0)
            {
                if (randomPos <= -1)
                {
                    Instantiate(circle, objectPos, transform.rotation);
                }
                else
                {
                    Instantiate(circle2, objectPos, transform.rotation);
                }
                
                //Instantiate(circle, objectPosRight, transform.rotation);
            }
            else
            {
                if(randomPos <= -1)
                {
                    Instantiate(obstacle, objectPos, transform.rotation);
                }
                else
                {
                    Instantiate(obstacle2, objectPos, transform.rotation);
                }
                
                //Instantiate(obstacle, objectPosRight, transform.rotation);
            }
            
            timer = delayTimer;
        }
        
    }
}

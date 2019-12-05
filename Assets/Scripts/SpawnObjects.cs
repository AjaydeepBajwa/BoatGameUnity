using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject circle;
    public GameObject obstacle;
    public float minPos;
    public float maxPos;
    // Start is called before the first frame update
    void Start()
    {
        //Random random = new Random();
        float[] XPosArray = new float[] { -2.75f, -1, 1, 2.75f};
        //int randomIndex = random.(0,XPosArray.Length - 1);
        //int randomPrice = priceArray[randomIndex];
        //Vector2 objectPosition = new Vector2(Random.;
        float randomPos = (XPosArray[Random.Range(0, XPosArray.Length)]);
        Vector2 objectPosition = new Vector2(randomPos, transform.position.y);

        Instantiate(circle,objectPosition, transform.rotation);
        Instantiate(obstacle, objectPosition, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

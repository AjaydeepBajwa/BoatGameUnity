using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject circle;
    public GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(circle, transform.position, transform.rotation);
        Instantiate(obstacle, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

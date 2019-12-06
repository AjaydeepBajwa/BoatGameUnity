﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Obstacle"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Circle")
        {
            SceneManager.LoadScene(1);
        }
    }
}

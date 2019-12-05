﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
   
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Debug.Log("Paused Game");
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            Debug.Log("Resumed Game");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int score;
    public Text txtScore;
    bool gotCircle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
    }
    public void addScore()
    {
        score++;
        txtScore.text = "" + score;
    }

    public void PlaySingle()
    {
        Debug.Log("Loaded 1P Scene");
        Application.LoadLevel("1PScene");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //public UIManager uiManager;
    int score;
    public Text txtFinalScore;
    // Start is called before the first frame update
    void Start()
    {
        //uiManager = GetComponent<UIManager>();
        score = PlayerPrefs.GetInt("score");
        Debug.Log("Final Score: " +score);
        txtFinalScore.text = "SCORE: " +score;
    }

    // Update is called once per frame
    void Update()
    {
        txtFinalScore.text = "SCORE: " + score;
        //uiManager = GetComponent<UIManager>();
        //Debug.Log("Final Score: " +score);
        //txtFinalScore.text = uiManager.txtScore.text;
    }
    //private void OnEnable()
    //{
    //    score = PlayerPrefs.GetInt("score");
    //}

    public void GotoHome()
    {
        SceneManager.LoadScene(2);
    }
}

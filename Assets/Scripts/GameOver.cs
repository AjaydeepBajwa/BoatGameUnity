using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //public UIManager uiManager;
    int score;
    int highScore;
    public Text txtFinalScore;
    public Text txtHighScore;
    // Start is called before the first frame update
    void Start()
    {
        //uiManager = GetComponent<UIManager>();
        highScore = PlayerPrefs.GetInt("highScore");
        score = PlayerPrefs.GetInt("score");
        if (score > highScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            highScore = PlayerPrefs.GetInt("highScore");
        }
        Debug.Log("Final Score: " +score);
        txtFinalScore.text = "SCORE: " +score;
        txtHighScore.text = "HIGH: " + highScore;
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
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerMulti : MonoBehaviour
{
    public int score = 0;
    public int score2 = 0;
    public Text txtScore;
    public Text txtScore2;

    public AudioManager audioMan;

    // Start is called before the first frame update
    void Start()
    {
        audioMan.bgMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
    }

    public void addScore()
    {
        score++;
        txtScore.text = "" +score;
        Debug.Log("Score isss: " + score);
    }

    public void addScore2()
    {
        score2++;
        txtScore2.text = "" + score2;
    }

    public void playBoatMoveSound()
    {
        audioMan.boatMoveSound.Play();
    }

}

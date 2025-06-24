using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text textScore;
    private int score;
    public static ScoreController instance;

    private void Awake()
    {
        instance = this;
    }

    public void GetScore(int score)
    {
        this.score += score;
        textScore.text = "Score : " + this.score.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private int diametroDoCampo;
    public TextMeshProUGUI ScoreText;
    private int score = 0;
    public TextMeshProUGUI HighScoreText;
    private int highScore = 0;
    private void Start()
    {
        ScoreText.gameObject.SetActive(true);
        UpdateScore(0);
        HighScoreText.gameObject.SetActive(true);
    }
    public void UpdateScore(int points)
    {
        score += points;
        ScoreText.text = "Score: " + score.ToString();
        if (score > highScore)
        {
            highScore = score;
            HighScoreText.text = "High Score: " + highScore.ToString();
        }

    }
        public void DefinirDIametro(string value)
    {
        diametroDoCampo = int.Parse(value);
    }
    public void DefinirVelocidade(string value)
    {
        GameObject.Find("snakeHead").GetComponent<SnakeMove>().speed = float.Parse(value);
    }
}

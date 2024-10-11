using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour //Define a pontuação e guarda as informações escritas pelo jogador 
{
    private int diametroDoCampo;
    public TextMeshProUGUI ScoreText;
    private int score = 0;
    public TextMeshProUGUI HighScoreText;
    private int highScore = 0;
    private GameManager instance;
    
    #region 
    private void Awake()//Método para instanciar o gameManager
    {
       instance = this;
    }
    #endregion //Padrão singleton
    private void Start()//Método para ativar o texto do score e do High Score 
    {
        ScoreText.gameObject.SetActive(true);
        UpdateScore(0);
        HighScoreText.gameObject.SetActive(true);
    }
    public void UpdateScore(int points) //Método para adicionais pontos ao score e veridicar se o High Score é maior que o scre
    {
        score += points;
        ScoreText.text = "Score: " + score.ToString();
        if (score > highScore)
        {
            highScore = score;
            HighScoreText.text = "High Score: " + highScore.ToString();
        }
    }
        public void DefinirDIametro(string value)//Método para o jogador definir o diametro do mapa
    {
        diametroDoCampo = int.Parse(value);
    }
    public void DefinirVelocidade(string value)//Método para o jogador definir a velocidade da cobrinha
    {
        GameObject.Find("snakeHead").GetComponent<SnakeMove>().speed = float.Parse(value);
    }
}

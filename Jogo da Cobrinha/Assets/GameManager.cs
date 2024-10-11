using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour //Define a pontua��o e guarda as informa��es escritas pelo jogador 
{
    private int diametroDoCampo;
    public TextMeshProUGUI ScoreText;
    private int score = 0;
    public TextMeshProUGUI HighScoreText;
    private int highScore = 0;
    private GameManager instance;
    
    #region 
    private void Awake()//M�todo para instanciar o gameManager
    {
       instance = this;
    }
    #endregion //Padr�o singleton
    private void Start()//M�todo para ativar o texto do score e do High Score 
    {
        ScoreText.gameObject.SetActive(true);
        UpdateScore(0);
        HighScoreText.gameObject.SetActive(true);
    }
    public void UpdateScore(int points) //M�todo para adicionais pontos ao score e veridicar se o High Score � maior que o scre
    {
        score += points;
        ScoreText.text = "Score: " + score.ToString();
        if (score > highScore)
        {
            highScore = score;
            HighScoreText.text = "High Score: " + highScore.ToString();
        }
    }
        public void DefinirDIametro(string value)//M�todo para o jogador definir o diametro do mapa
    {
        diametroDoCampo = int.Parse(value);
    }
    public void DefinirVelocidade(string value)//M�todo para o jogador definir a velocidade da cobrinha
    {
        GameObject.Find("snakeHead").GetComponent<SnakeMove>().speed = float.Parse(value);
    }
}

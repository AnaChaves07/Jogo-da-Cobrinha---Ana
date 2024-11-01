using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeMove : MonoBehaviour //Movimentação da cobrinha + métodos de game over e reniciar jogo
{
    public Transform segmentPrefab;
    public Vector2Int direction;
    public float speed; 
    public float speedMultiplier = 1f;
    public int initialSize = 1;
    private List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;
    public GameObject gameOverPanel;
    public GameObject initialPanel;
    public GameManager gameManager;
    private bool isGrowing = false;
    private bool isTeleporting = false;
    private float teleportDuration = 0.1f; 
    private float teleportEndTime = 0f; 


    private void Awake() //Método para que tempo do jogo não seja pausado ao iniciar.
    {
        Time.timeScale = 1f;
    }
    private void Start()
    {
        ResetState();
    }
    private void Update() //Movimentação da cobrinha
    {
        if (isTeleporting && Time.time >= teleportEndTime)
        {
            isTeleporting = false; // Reseta a flag após o tempo
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = Vector2Int.down;
            }        
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = Vector2Int.right;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                direction = Vector2Int.left;
            }
    }
    private void FixedUpdate() //Método que move a cobra e atualiza a posição de cada segmento
    {
        if (Time.time < nextUpdate)
        {
            return;
        }

        if (isGrowing)
        {
            Grow();
            isGrowing = false; 
        }
        if (input != Vector2Int.zero)
        {
            direction = input;
        }
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        int x = Mathf.RoundToInt(transform.position.x) + direction.x;
        int y = Mathf.RoundToInt(transform.position.y) + direction.y;
        transform.position = new Vector2(x, y);

        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }
    public void Grow() //Método para crescer a cobra 
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);

    }
    public void ResetState() //Reseta a cobra para o estado inicial dps de reniciar o jogo
    {
        direction = Vector2Int.right;
        transform.position = Vector3.zero;
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }
    }
    public bool Occupies(int x, int y) //Verifica se a posição ocupada pelo segmento da cobra.
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x && Mathf.RoundToInt(segment.position.y) == y)
            {
                return true;
            }
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D other) //Método para checar as colisões 

    {
        if (isTeleporting)
        {
            return; // Ignora colisões enquanto está teleportando
        }

        if (other.gameObject.CompareTag("Apple"))
        {
            Grow();
            gameManager.UpdateScore(1);
            isGrowing = true;
        }
        else if (other.gameObject.CompareTag("limite"))
        {    
                Teletransport(other.transform);
        }
        else if (other.gameObject.CompareTag("snakeTail")) 
        {
            if (!isGrowing && Occupies(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)))
            {
                GameOver(); 
            }
        }
    }
    private void Teletransport(Transform wall) //Teletransporte da cobrinha
    {
        isTeleporting = true;
        Vector3 position = transform.position;
        teleportEndTime = Time.time + teleportDuration;

        if (direction.x > 0) 
        {
            position.x = -Camera.main.orthographicSize * Camera.main.aspect; 
        }
        else if (direction.x < 0) 
        {
            position.x = Camera.main.orthographicSize * Camera.main.aspect; 
        }
        else if (direction.y > 0) 
        {
            position.y = -Camera.main.orthographicSize; 
        }
        else if (direction.y < 0)
        { 
            position.y = Camera.main.orthographicSize; 
        }
        transform.position = position;
       // Invoke("ResetTeleportingFlag", 0.1f);

    }
    private void ResetTeleportingFlag()
    {
        isTeleporting = false;
    }
    void GameOver() //Método de game Over
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void ReniciarJogo() //Método para reniciar jogo
    {
        SceneManager.LoadScene("Jogo");
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;  
    }
    public void PlayButton() //Botão de play 
    {
        initialPanel.SetActive(false);     
    }

}

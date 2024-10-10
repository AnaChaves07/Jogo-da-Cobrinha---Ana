using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeMove : MonoBehaviour
{
    public Transform segmentPrefab;
    public Vector2Int direction;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    public int initialSize = 1;
    private List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;
    public GameObject gameOverPanel;

    private void Start()
    {
        ResetState();
    }
    private void Update()
    {
       
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

    private void FixedUpdate()
    {
        if (Time.time < nextUpdate)
        {
            return;
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

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState()
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
    public bool Occupies(int x, int y)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            Grow();
        }
        else if (other.gameObject.CompareTag("limite"))
        {    
                Teletransport(other.transform);
        }
        else if (other.gameObject.CompareTag("snakeTail")) 
        {
            GameOver();
            Debug.Log("Colidir");
        }
    }
    private void Teletransport(Transform wall)
    {
        Vector3 position = transform.position;

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
    }
    void GameOver()
    {
       // SceneManager.LoadScene("GameOver");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
   
}

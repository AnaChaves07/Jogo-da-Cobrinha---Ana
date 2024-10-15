using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnApple : MonoBehaviour //Spawna as ma��s 
{
    public Collider2D gridArea;
    private SnakeMove snake;

    private void Awake() //M�todo para encontrar e armazenar a refer�ncia da cobra.
    {
        snake = FindObjectOfType<SnakeMove>();
    }
    private void Start()//M�todo para iniciar o jogo instanciando a ma��
    {
        RandomizePosition();
    }
    public void RandomizePosition() //M�todo para definir uma posi��o aleat�ria para a ma�� dentro do gridArea
    {
        Bounds bounds = gridArea.bounds;

        int x = Mathf.RoundToInt(Random.Range(bounds.min.x, bounds.max.x));
        int y = Mathf.RoundToInt(Random.Range(bounds.min.y, bounds.max.y));

        while (snake.Occupies(x, y))
        {
            x++;
            if (x > bounds.max.x)
            {
                x = Mathf.RoundToInt(bounds.min.x);
                y++;
                if (y > bounds.max.y)
                {
                    y = Mathf.RoundToInt(bounds.min.y);
                }
            }
        }
        transform.position = new Vector2(x, y);
    }
    private void OnTriggerEnter2D(Collider2D other) //M�todo para definir uma posi��o aleat�ria pra ma�� depois da colis�o
    {
        RandomizePosition();
    }
}


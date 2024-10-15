using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnApple : MonoBehaviour //Spawna as maçãs 
{
    public Collider2D gridArea;
    private SnakeMove snake;

    private void Awake() //Método para encontrar e armazenar a referência da cobra.
    {
        snake = FindObjectOfType<SnakeMove>();
    }
    private void Start()//Método para iniciar o jogo instanciando a maçã
    {
        RandomizePosition();
    }
    public void RandomizePosition() //Método para definir uma posição aleatória para a maçã dentro do gridArea
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
    private void OnTriggerEnter2D(Collider2D other) //Método para definir uma posição aleatória pra maçã depois da colisão
    {
        RandomizePosition();
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    private const float velocidade = 2f;
    private Vector2 direcao;

    private void Update()
    {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                direcao.x = -1;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                direcao.x = 1;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))

            {
                direcao.y = 1;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                direcao.y = -1;
            }
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }

    }

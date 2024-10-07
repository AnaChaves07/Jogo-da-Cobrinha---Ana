using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector2 screenBounds;
    float score;

    public static GameManager instance;

    public Vector2 ScreenBounds { get => screenBounds; }

    private void Awake()
    {
        instance = this;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height)) + new Vector3(-1, 1);
    }

    public void AddScore(int value)
    {
        score += value;
    }
}

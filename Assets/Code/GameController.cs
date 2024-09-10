using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public double money = 0;
    public double health = 100;

    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("GameController");
                instance = go.AddComponent<GameController>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

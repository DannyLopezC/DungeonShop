﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;

    //referenced in inspector
    public Player player;

    public FloatingTextManager ftm;

    public int money;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration) =>
        ftm.Show(msg, fontSize, color, position, motion, duration);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Game Object")]
    public Player player;
    public PoolMgr PoolMgr;

    // Game System
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10, 30, 60, 100, 150, 210, 360, 450, 600 };

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    // 플레이어가 경험치를 얻고 레벨업하는 함수
    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }

}

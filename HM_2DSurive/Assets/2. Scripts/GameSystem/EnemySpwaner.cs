// EnemySpawner.cs
// 적을 자동으로 생성해주는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    public SpwanData[] spwanDatas;
    public Transform[] spwanPoint;
    
    int level; // 스테이지 레벨 변수

    float timer;
    public float spwanTime; // 스폰 시간 조절용
    private void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();  // 미리 생성해둔 스폰 포인트들의 Transform 가져오기
    }

    void Update()
    {
        timer += Time.deltaTime;

        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spwanDatas.Length - 1); // 게임시간 / 10을 함으로써 10초마다 level이 1씩 올라감 
                                                                                                         // Mathf.FloorToTint => 소수점 자리를 모두 날림
                                                                                                         // Mathf.Min(A,B) => A와 B 둘 중 더 작은 값을 반환

        if(timer > (spwanDatas[level].spwanTIme))
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
       GameObject enemy = GameManager.instance.enemyPoolMgr.Get(0); 

        enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;

        enemy.GetComponent<EnemyMove>().Init(spwanDatas[level]);
    }
}

// 적들의 데이터를 세팅하는 클래스
[System.Serializable]
public class SpwanData
{
    public float spwanTIme; // 몬스터의 스폰 시간
    public int spriteType; // 스프라이트 타입에 따라 적들이 달라짐
    public int health;      // 몬스터의 현재체력
    public float speed;     // 몬스터의 속도
}
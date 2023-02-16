// EnemySpawner.cs
// 적을 자동으로 생성해주는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    public Transform[] spwanPoint;

    float timer;
    public float spwanTime; // 스폰 시간 조절용
    private void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();  // 미리 생성해둔 스폰 포인트들의 Transform 가져오기
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > spwanTime)
        {
            timer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
       GameObject enemy = GameManager.instance.enemyPoolMgr.Get(Random.Range(0,2)); // EnemyPoolMgr에서 저장된 프리펩중 랜덤으로 하나를 선택
                                                                                    // 비활성화 되어 있는 오브젝트가 있을 경우 활성화
                                                                                    // 비활성화 되어 있는 오브젝트가 없을 경우 새로 생성

        enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
    }
}

// EnemySpawner.cs
// ���� �ڵ����� �������ִ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    public Transform[] spwanPoint;

    float timer;
    public float spwanTime; // ���� �ð� ������
    private void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();  // �̸� �����ص� ���� ����Ʈ���� Transform ��������
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
       GameObject enemy = GameManager.instance.enemyPoolMgr.Get(Random.Range(0,2)); // EnemyPoolMgr���� ����� �������� �������� �ϳ��� ����
                                                                                    // ��Ȱ��ȭ �Ǿ� �ִ� ������Ʈ�� ���� ��� Ȱ��ȭ
                                                                                    // ��Ȱ��ȭ �Ǿ� �ִ� ������Ʈ�� ���� ��� ���� ����

        enemy.transform.position = spwanPoint[Random.Range(1, spwanPoint.Length)].position;
    }
}

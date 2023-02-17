// EnemySpawner.cs
// ���� �ڵ����� �������ִ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    public SpwanData[] spwanDatas;
    public Transform[] spwanPoint;
    
    int level; // �������� ���� ����

    float timer;
    public float spwanTime; // ���� �ð� ������
    private void Awake()
    {
        spwanPoint = GetComponentsInChildren<Transform>();  // �̸� �����ص� ���� ����Ʈ���� Transform ��������
    }

    void Update()
    {
        timer += Time.deltaTime;

        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spwanDatas.Length - 1); // ���ӽð� / 10�� �����ν� 10�ʸ��� level�� 1�� �ö� 
                                                                                                         // Mathf.FloorToTint => �Ҽ��� �ڸ��� ��� ����
                                                                                                         // Mathf.Min(A,B) => A�� B �� �� �� ���� ���� ��ȯ

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

// ������ �����͸� �����ϴ� Ŭ����
[System.Serializable]
public class SpwanData
{
    public float spwanTIme; // ������ ���� �ð�
    public int spriteType; // ��������Ʈ Ÿ�Կ� ���� ������ �޶���
    public int health;      // ������ ����ü��
    public float speed;     // ������ �ӵ�
}
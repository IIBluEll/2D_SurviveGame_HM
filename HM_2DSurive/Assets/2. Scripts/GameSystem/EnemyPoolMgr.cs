// EnemyPoolMgr.cs
// ������Ʈ Ǯ���� ���� Enemy Ȱ��/��Ȱ���� �����ϴ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolMgr : MonoBehaviour
{
    public GameObject[] enemyPrefebs;   

    List<GameObject>[] enemyPool;       

    private void Awake()
    {
        enemyPool = new List<GameObject>[enemyPrefebs.Length]; 

        for(int i = 0; i < enemyPrefebs.Length; i++)
            enemyPool[i] = new List<GameObject>();      // enemyPool List �ʱ�ȭ
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in enemyPool[index])
        {
            if(!item.activeSelf)
            // ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� ������� select�� �Ҵ�
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            // ��Ȱ��ȭ �Ǿ��ִ� ������Ʈ�� ���� ��� ���� �����ؼ� select�� �Ҵ�
            select = Instantiate(enemyPrefebs[index], transform);
            enemyPool[index].Add(select);
        }

        return select;
    }
}

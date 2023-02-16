// EnemyPoolMgr.cs
// 오브젝트 풀링을 통해 Enemy 활성/비활성을 제어하는 스크립트

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
            enemyPool[i] = new List<GameObject>();      // enemyPool List 초기화
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in enemyPool[index])
        {
            if(!item.activeSelf)
            // 비활성화 되어있는 오브젝트가 있을경우 select에 할당
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(!select)
        {
            // 비활성화 되어있는 오브젝트가 없을 경우 새로 생성해서 select에 할당
            select = Instantiate(enemyPrefebs[index], transform);
            enemyPool[index].Add(select);
        }

        return select;
    }
}

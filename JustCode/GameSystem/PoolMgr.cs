// PoolMgr.cs
// 오브젝트 풀링을 통해 적, 무기 등 오브젝트들을 관리하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : MonoBehaviour
{
    public GameObject[] Prefebs;   

    List<GameObject>[] Pool;       

    private void Awake()
    {
        Pool = new List<GameObject>[Prefebs.Length]; 

        for(int i = 0; i < Prefebs.Length; i++)
            Pool[i] = new List<GameObject>();      // Pool List 초기화
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in Pool[index])
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
            select = Instantiate(Prefebs[index], transform);
            Pool[index].Add(select);
        }

        return select;
    }
}

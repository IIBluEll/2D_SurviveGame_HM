// PoolMgr.cs
// ������Ʈ Ǯ���� ���� ��, ���� �� ������Ʈ���� �����ϴ� ��ũ��Ʈ

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
            Pool[i] = new List<GameObject>();      // Pool List �ʱ�ȭ
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in Pool[index])
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
            select = Instantiate(Prefebs[index], transform);
            Pool[index].Add(select);
        }

        return select;
    }
}

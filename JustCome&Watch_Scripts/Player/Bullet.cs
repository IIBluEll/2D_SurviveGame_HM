// Bullet.cs
// 무기 변수 관련 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; // 데미지 변수
    public int per;    // 관통 갯수 변수

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 데미지와 관통 초기화, 방향
    public void Init(float damage, int per, Vector3 dir)  
    {
        this.damage = damage;
        this.per = per;

        if (per > -1) // 관통력이 0 미만은 근접 무기 / 이상은 원거리 무기
            rigid.velocity = dir * 15f;
    }

    // 몬스터를 관통했을 경우 관통 갯수에 따라 상태 변환
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || (per == -1))
            return;

        per--;

        if(per == -1)
        {
            rigid.velocity = Vector2.zero;  
            gameObject.SetActive(false);
        }
    }
}

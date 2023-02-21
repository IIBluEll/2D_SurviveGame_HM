// EnemyMove.cs
// Enemy�� �����ϴ� ��ũ��Ʈ

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;

    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D enemyRigid;
    SpriteRenderer enemySpriter;
    Animator anim;

    public void Init(SpwanData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnEnable()
    {
        isLive = true;
        health = maxHealth;
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyRigid = GetComponent<Rigidbody2D>();
        enemySpriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(!isLive) return;

        Vector2 dirVec = target.position - enemyRigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        enemyRigid.MovePosition(enemyRigid.position + nextVec);
        enemyRigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive) return;

        enemySpriter.flipX = target.position.x < enemyRigid.position.x; // �÷��̾�� ���� X�� �񱳸� ���� flipX�� �ٲ۴�
    }

    // Enemy�� ����� �浹���� ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0)
        {

        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        #region �ӽ� ���� �ڵ�
        gameObject.SetActive(false);
        #endregion
    }
}

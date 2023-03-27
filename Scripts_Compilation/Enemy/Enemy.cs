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
    public int kuckBackMount;

    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D enemyRigid;
    //Collider2D enemyColl;
    CapsuleCollider2D enemyColl;
    SpriteRenderer enemySpriter;
    Animator enemyAnim;
    WaitForFixedUpdate wait;

    public void Init(SpwanData data)
    {
        enemyAnim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemyRigid = GetComponent<Rigidbody2D>();
        enemyColl = GetComponent<CapsuleCollider2D>();
        enemySpriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();

        isLive = true;
        enemyColl.enabled = true;
        enemyRigid.simulated = true;
        enemyAnim.SetBool("Dead", false);
        enemySpriter.sortingOrder = 2;

        health = maxHealth;
    }

    private void FixedUpdate()
    {
        // ���� �׾��ų� || ���� �ִϸ������� ���°� Hit�� ��� ��ȯ
        if(!isLive || enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;    

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
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;

        StartCoroutine(KnockBack());

        if(health > 0)
        {
            // �ǰ� ���� �ִϸ��̼�
            enemyAnim.SetTrigger("Hit");
        }
        else
        {
            // �׾������� ��� ����
            isLive = false;
            enemyColl.enabled = false;
            enemyRigid.simulated = false;

            // ��ü�� �ٸ� ������Ʈ�� ������ �ʵ��� �ϱ� ����
            enemySpriter.sortingOrder = 1;  
            enemyAnim.SetBool("Dead", true);

            // GameManager�� ����ġ �ý��� 
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        // �÷��̾� �ݴ� �������� �˹� 
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        enemyRigid.AddForce(dirVec.normalized * kuckBackMount, ForceMode2D.Impulse);
    }

    // �ִϸ��̼� �̺�Ʈ�� ���� ȣ��
    private void Dead()
    {
        #region �ӽ� ���� �ڵ�
        gameObject.SetActive(false);
        #endregion
    }
}

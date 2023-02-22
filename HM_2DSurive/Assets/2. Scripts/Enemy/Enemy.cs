// EnemyMove.cs
// Enemy를 관리하는 스크립트

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
        // 적이 죽었거나 || 현재 애니메이터의 상태가 Hit일 경우 반환
        if(!isLive || enemyAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;    

        Vector2 dirVec = target.position - enemyRigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        enemyRigid.MovePosition(enemyRigid.position + nextVec);
        enemyRigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!isLive) return;

        enemySpriter.flipX = target.position.x < enemyRigid.position.x; // 플레이어와 적의 X축 비교를 통해 flipX를 바꾼다
    }

    // Enemy가 무기와 충돌했을 때
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;

        StartCoroutine(KnockBack());

        if(health > 0)
        {
            // 피격 판정 애니메이션
            enemyAnim.SetTrigger("Hit");
        }
        else
        {
            // 죽었음으로 기능 정지
            isLive = false;
            enemyColl.enabled = false;
            enemyRigid.simulated = false;

            // 시체가 다른 오브젝트를 가리지 않도록 하기 위함
            enemySpriter.sortingOrder = 1;  
            enemyAnim.SetBool("Dead", true);

            // GameManager의 경험치 시스템 
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        // 플레이어 반대 방향으로 넉백 
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;

        enemyRigid.AddForce(dirVec.normalized * kuckBackMount, ForceMode2D.Impulse);
    }

    // 애니메이션 이벤트를 통해 호출
    private void Dead()
    {
        #region 임시 죽음 코드
        gameObject.SetActive(false);
        #endregion
    }
}

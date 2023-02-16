// EnemyMove.cs
// 적이 플레이어를 추적하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D enemyRigid;
    SpriteRenderer enemySpriter;

    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
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

        enemySpriter.flipX = target.position.x < enemyRigid.position.x; // 플레이어와 적의 X축 비교를 통해 flipX를 바꾼다
    }
}

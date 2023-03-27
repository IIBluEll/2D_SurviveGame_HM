// RePosition.cs
// 플레이어가 이동함에 따라 타일맵 또는 적을 재배치 하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = this.transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.instance.player.inputVec;

        // 플레이어의 방향 구하기
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            // 타일맵이 멀어졌을 경우
            case "Ground":

                // 플레이어가 대각선으로 움직일 경우 ( 개선 필요 )
                if(Mathf.Abs(diffX-diffY) <= 0.1f)
                { transform.Translate(dirX * 40, dirY * 40, 0); }

                // 플레이어가 X축으로 더 많이 움직일 경우 X축으로 이동
                else if (diffX > diffY)
                { transform.Translate(dirX * 40, 0, 0); }

                // 플레이어가 Y축으로 더 많이 움직일 경우 Y축으로 이동
                else if (diffX < diffY)
                { transform.Translate(0, dirY * 40, 0); }

                break;

            // 적이 멀어졌을 경우
            case "Enemy":

                if(coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}


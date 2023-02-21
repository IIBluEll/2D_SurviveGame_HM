// RePosition.cs
// �÷��̾ �̵��Կ� ���� Ÿ�ϸ� �Ǵ� ���� ���ġ �ϴ� ��ũ��Ʈ

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

        // �÷��̾��� ���� ���ϱ�
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            // Ÿ�ϸ��� �־����� ���
            case "Ground":

                // �÷��̾ �밢������ ������ ��� ( ���� �ʿ� )
                if(Mathf.Abs(diffX-diffY) <= 0.1f)
                { transform.Translate(dirX * 40, dirY * 40, 0); }

                // �÷��̾ X������ �� ���� ������ ��� X������ �̵�
                else if (diffX > diffY)
                { transform.Translate(dirX * 40, 0, 0); }

                // �÷��̾ Y������ �� ���� ������ ��� Y������ �̵�
                else if (diffX < diffY)
                { transform.Translate(0, dirY * 40, 0); }

                break;

            // ���� �־����� ���
            case "Enemy":

                if(coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}


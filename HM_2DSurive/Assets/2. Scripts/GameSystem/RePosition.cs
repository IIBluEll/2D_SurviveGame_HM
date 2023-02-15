using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RePosition.cs
// �÷��̾ �̵��Կ� ���� Ÿ�ϸ� �Ǵ� ���� ���ġ �ϴ� ��ũ��Ʈ



public class RePosition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = this.transform.position;

        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        print("CollName = " + collision.gameObject.name + " diffX = " + diffX + " diffY = " + diffY);

        switch (transform.tag)
        {
            case "Ground":

                if (diffX > diffY)
                { transform.Translate(dirX * 40, 0, 0); }

                else if (diffX < diffY)
                { transform.Translate(0, dirY * 40, 0); }

                else
                {
                    print("(X,Y,0)");
                    transform.Translate(dirX * 40, dirY * 40, 0);
                }

                break;

            case "Enemy":
                break;
        }
    }


}


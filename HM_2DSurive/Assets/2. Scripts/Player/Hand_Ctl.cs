// Hand_Ctl.cs
// ĳ���Ͱ� �����Ǿ��� ��, �տ� �� ���⵵ �ڿ������� �����ǵ��� �ϴ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Ctl : MonoBehaviour
{
    public bool isLeft; // �� ���� �޼����� ���������� ������ ���� bool ����
    public SpriteRenderer spriter;  

    SpriteRenderer player;  // �÷��̾ flip�� �ߴ��� Ȯ���ϱ� ����

    Vector3 rightPos = new Vector3(0.35f,-0.15f,0 );        // �������� �� ��ġ ( �������� �ʾ��� �� )
    Vector3 rightPosReverse = new Vector3(-0.15f,-0.15f,0); // ������ �������� ��ġ
    Quaternion leftRot = Quaternion.Euler(0,0,-35);         // �޼��� �� ȸ���� Quatanion ���·� ����
    Quaternion leftRotReverse = Quaternion.Euler(0,0,-135); // ������ �޼��� �� ȸ��

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; // �÷��̾��� ��������Ʈ������ �ʱ�ȭ
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;  // �÷��̾��� ���� ���¸� ���������� ���� 

        // �� ���� �޼����� Ȯ��
        if (isLeft)  
        {   // ���� ����
            transform.localRotation = isReverse ? leftRotReverse : leftRot; // �����Ǿ��ִ��� ? ������ ���
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;   // �����Ǿ��� ��, �ڿ������� ���� ���������� Order ����
        }
        else
        {   // ���Ÿ� ����
            transform.localPosition = isReverse ? rightPosReverse : rightPos; // �����Ǿ��ִ��� ? ������ ���
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;   // �����Ǿ��� ��, �ڿ������� ���� ���������� Order ����
        }


    }

}

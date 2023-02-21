//Scanner.cs
// ���Ÿ� ������ ���� ����� ���� ã�� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;         // ���� ����
    public LayerMask targetLayer;   // ���̾� ����ũ
    public RaycastHit2D[] targets;  // ����ĳ��Ʈ �浹 ����� ���� �迭
    public Transform nearestTarget; // ���� ����� Ÿ���� Transform

    private void FixedUpdate()
    {
        // ������ ĳ��Ʈ�� ��� ����� ��ȯ ( ĳ���� ���� ��ġ / ���� ������ / ĳ���� ���� / ĳ���� ���� / ��� ���̾� )
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer); 

        nearestTarget = GetNearest();
    }

    // ���� ����� Ÿ�� Transform ��ȯ
    Transform GetNearest()
    {
        Transform result = null;

        float diff = 100;               // �Ÿ� ������

        // ĳ���� �� Ÿ�ٵ��� ���� ����� Ÿ���� �����ϱ� ���� ���������� �Ÿ� �˻�
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetpos = target.transform.position;

            float curDiff = Vector3.Distance(myPos, targetpos);

            // ���� Ÿ���� ������ ���� ����� ��� => �� �Ÿ��� �������� ��
            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
}

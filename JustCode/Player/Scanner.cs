//Scanner.cs
// 원거리 공격을 위해 가까운 적을 찾는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;         // 추적 범위
    public LayerMask targetLayer;   // 레이어 마스크
    public RaycastHit2D[] targets;  // 레이캐스트 충돌 결과를 담을 배열
    public Transform nearestTarget; // 가장 가까운 타겟의 Transform

    private void FixedUpdate()
    {
        // 원형의 캐스트를 쏘고 결과를 반환 ( 캐스팅 시작 위치 / 원의 반지름 / 캐스팅 방향 / 캐스팅 길이 / 대상 레이어 )
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer); 

        nearestTarget = GetNearest();
    }

    // 가장 가까운 타겟 Transform 반환
    Transform GetNearest()
    {
        Transform result = null;

        float diff = 100;               // 거리 기준점

        // 캐스팅 된 타겟들중 가장 가까운 타겟을 선택하기 위해 순차적으로 거리 검사
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetpos = target.transform.position;

            float curDiff = Vector3.Distance(myPos, targetpos);

            // 현재 타겟이 기준점 보다 가까울 경우 => 이 거리가 기준점이 됨
            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
}

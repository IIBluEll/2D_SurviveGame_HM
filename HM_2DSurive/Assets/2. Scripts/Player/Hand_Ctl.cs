// Hand_Ctl.cs
// 캐릭터가 반전되었을 때, 손에 든 무기도 자연스럽게 반전되도록 하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Ctl : MonoBehaviour
{
    public bool isLeft; // 이 손이 왼손인지 오른손이지 구별을 위한 bool 변수
    public SpriteRenderer spriter;  

    SpriteRenderer player;  // 플레이어가 flip을 했는지 확인하기 위함

    Vector3 rightPos = new Vector3(0.35f,-0.15f,0 );        // 오른손의 각 위치 ( 반전되지 않았을 때 )
    Vector3 rightPosReverse = new Vector3(-0.15f,-0.15f,0); // 반전된 오른손의 위치
    Quaternion leftRot = Quaternion.Euler(0,0,-35);         // 왼손의 각 회전을 Quatanion 형태로 저장
    Quaternion leftRotReverse = Quaternion.Euler(0,0,-135); // 반전된 왼손의 각 회전

    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1]; // 플레이어의 스프라이트렌더러 초기화
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;  // 플레이어의 반전 상태를 지역변수로 저장 

        // 이 손이 왼손인지 확인
        if (isLeft)  
        {   // 근접 무기
            transform.localRotation = isReverse ? leftRotReverse : leftRot; // 반전되어있는지 ? 연산자 사용
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;   // 반전되었을 때, 자연스럽게 몸에 가려지도록 Order 변경
        }
        else
        {   // 원거리 무기
            transform.localPosition = isReverse ? rightPosReverse : rightPos; // 반전되어있는지 ? 연산자 사용
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;   // 반전되었을 때, 자연스럽게 몸에 가려지도록 Order 변경
        }


    }

}

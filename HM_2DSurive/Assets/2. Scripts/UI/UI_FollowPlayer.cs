using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FollowPlayer : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
            //WOrldToScreenPoint => 월드 상의 오브젝트 위치를 스크린 좌표로 변환
            // UI와 오브젝트간의 좌표가 다르기 때문에 이런식의 변환이 필요
    }
}

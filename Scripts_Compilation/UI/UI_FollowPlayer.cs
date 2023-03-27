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
            //WOrldToScreenPoint => ���� ���� ������Ʈ ��ġ�� ��ũ�� ��ǥ�� ��ȯ
            // UI�� ������Ʈ���� ��ǥ�� �ٸ��� ������ �̷����� ��ȯ�� �ʿ�
    }
}

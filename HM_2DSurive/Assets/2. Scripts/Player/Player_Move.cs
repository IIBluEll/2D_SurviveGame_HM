//Player_Move.cs
// �÷��̾��� �̵��� �ִϸ��̼��� ����ϴ� ��ũ��Ʈ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Move : MonoBehaviour
{
    public Vector2 inputVec;    // InputSystem���� ���� Value���� ������ Vector2 ����
    public float speed;

    Rigidbody2D playerRigid;
    SpriteRenderer playerSprite;
    Animator playerAnim;

    private void Awake()
    {
        playerRigid = this.GetComponent<Rigidbody2D>();
        playerSprite = this.GetComponent<SpriteRenderer>();
        playerAnim = this.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector2 moveVec = inputVec * speed * Time.fixedDeltaTime;
        playerRigid.MovePosition(playerRigid.position + moveVec);
    }

    private void LateUpdate()
    {
        if(inputVec.x != 0)                         // inputVec.x ���� 0�� �ƴ� ��� ( �÷��̾ ������ ��� )
        {
            playerSprite.flipX = inputVec.x < 0;    // inputVec.x ���� 0���� ���� ��� True => flipX = true
                                                    // inputVec.x ���� 0���� Ŭ ��� False  => flipx = fal
        }

        playerAnim.SetFloat("Speed", inputVec.magnitude);   // magnitude -> vector�� ���̸� ��ȯ
    }

    void OnMove(InputValue inputValue)      // InputSystem���� ȣ���� �Լ�
    {
        inputVec = inputValue.Get<Vector2>();   // InputSystem���� �޾ƿ� value�� Vector2 ������ ������ ����
    }
}

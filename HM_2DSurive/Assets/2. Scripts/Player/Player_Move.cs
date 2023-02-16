//Player_Move.cs
// 플레이어의 이동과 애니메이션을 담당하는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Move : MonoBehaviour
{
    public Vector2 inputVec;    // InputSystem에서 받은 Value값을 저장할 Vector2 변수
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
        if(inputVec.x != 0)                         // inputVec.x 값이 0이 아닐 경우 ( 플레이어가 움직일 경우 )
        {
            playerSprite.flipX = inputVec.x < 0;    // inputVec.x 값이 0보다 작을 경우 True => flipX = true
                                                    // inputVec.x 값이 0보다 클 경우 False  => flipx = fal
        }

        playerAnim.SetFloat("Speed", inputVec.magnitude);   // magnitude -> vector의 길이를 반환
    }

    void OnMove(InputValue inputValue)      // InputSystem에서 호출할 함수
    {
        inputVec = inputValue.Get<Vector2>();   // InputSystem에서 받아온 value를 Vector2 값으로 가져와 저장
    }
}

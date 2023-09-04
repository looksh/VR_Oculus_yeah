using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float gravity = -9.81f; // 중력 가속도

    Vector3 moveDirection;
    Vector3 velocity; // 수직 속도를 저장하기 위한 변수
    bool isGrounded; // 플레이어가 땅에 닿아 있는지 여부를 확인하기 위한 변수

    public CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(x, 0, z);

        // 플레이어가 땅에 닿아 있는지 여부를 확인
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
	    Debug.Log("바닥 닿았다");
            // 플레이어가 땅에 닿아 있는 경우에만 중력 적용
            velocity.y = -2.0f; // 땅에 닿아 있으므로 수직 속도를 초기화
        }
        else
        {
	    Debug.Log("바닥 안닿았다");
            // 플레이어가 땅에 닿아 있지 않으면 중력을 적용
            velocity.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float gravity = -9.81f; // �߷� ���ӵ�

    Vector3 moveDirection;
    Vector3 velocity; // ���� �ӵ��� �����ϱ� ���� ����
    bool isGrounded; // �÷��̾ ���� ��� �ִ��� ���θ� Ȯ���ϱ� ���� ����

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

        // �÷��̾ ���� ��� �ִ��� ���θ� Ȯ��
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
	    Debug.Log("�ٴ� ��Ҵ�");
            // �÷��̾ ���� ��� �ִ� ��쿡�� �߷� ����
            velocity.y = -2.0f; // ���� ��� �����Ƿ� ���� �ӵ��� �ʱ�ȭ
        }
        else
        {
	    Debug.Log("�ٴ� �ȴ�Ҵ�");
            // �÷��̾ ���� ��� ���� ������ �߷��� ����
            velocity.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}

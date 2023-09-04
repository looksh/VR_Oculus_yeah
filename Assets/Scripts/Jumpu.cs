using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpu : MonoBehaviour
{
    public PlayerState playerState = PlayerState.IDLE;
    public float gravity = -20; //�߷�

    public float jumpPower = 10; //���� �Ŀ�

    public float yVelocity; //���� ����

    private bool isJump = true; //���� ������ Ȯ���ϴ� ����

    private float mouseX; //���콺 X��

    private float mouseY; //���콺 Y��

    public float rotSpeed;//ī�޶� ȸ�� �ӵ�

    public Transform cam; //ī�޶�

    public float moveSpeed = 10; //�̵� �ӵ�

    public CharacterController cc; //ĳ���� ��Ʈ�ѷ�

    public enum PlayerState

    {

        IDLE, JUMPPOINT

    }

    private void Update()
    {
        CameraRotate(); //ī�޶� ȸ�� ���
        Move(); //�̵� ���
    }

    //ī�޶� ȸ�� �Լ�

    void CameraRotate()

    {

        float horizontal = Input.GetAxis("Mouse X"); //���콺 ���� �̵� �Է�

        float vertical = Input.GetAxis("Mouse Y"); //���콺 ���� �̵� �Է�

        //���콺 X���� ȸ�� ���� ����Ѵ�.

        mouseX += horizontal * rotSpeed * Time.deltaTime;

        //���콺 Y���� ȸ�� ���� ����Ѵ�.

        mouseY += vertical * rotSpeed * Time.deltaTime;

        //���콺 Y���� ���� -90���� 90���� �����Ѵ�.

        mouseY = Mathf.Clamp(mouseY, -90.0f, 90.0f);

        //���� ȸ�� �� ��ŭ ī�޶� ȸ����Ų��.

        cam.eulerAngles = new Vector3(-mouseY, mouseX, 0);

    }

    //�̵� �� ���� �Լ�

    void Move()

    {

        //���� ���� �Է�

        float horizontal = Input.GetAxis("Horizontal"); //���� �̵� �Է�

        float vertical = Input.GetAxis("Vertical"); //���� �̵� �Է�

        //ī�޶� �ٶ󺸴� �������� ���� ��ȯ

        Vector3 dir = new Vector3(horizontal, 0, vertical) * moveSpeed;

        dir = cam.TransformDirection(dir);

        switch (playerState)

        {

            case PlayerState.IDLE:

                {

                    //ĳ���Ͱ� �ٴڿ� �ִٸ�

                    if (cc.isGrounded)

                    {

                        yVelocity = 0;

                        //isJump�� false���

                        if (!isJump)

                        {

                            //isJump�� true�� �����Ѵ�.

                            isJump = true;

                        }

                    }

                    //isJump�� true�� ��

                    if (isJump)

                    {

                        //����

                        if (Input.GetButtonDown("Jump"))

                        {

                            yVelocity = jumpPower;

                            //isJump�� false�� �����Ѵ�.

                            isJump = false;

                        }

                    }

                    break;

                }

            case PlayerState.JUMPPOINT:

                {

                    Debug.Log(cc.isGrounded);

                    if (isJump)

                    {

                        yVelocity = jumpPower;

                        isJump = false;

                    }

                    if (cc.isGrounded)

                    {

                        jumpPower = 50;

                        playerState = PlayerState.IDLE;

                    }

                    break;

                }

        }

        //�߷� ���

        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;

        //�̵�

        cc.Move(dir * Time.deltaTime);

    }
}

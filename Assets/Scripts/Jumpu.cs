using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpu : MonoBehaviour
{
    public PlayerState playerState = PlayerState.IDLE;
    public float gravity = -20; //중력

    public float jumpPower = 10; //점프 파워

    public float yVelocity; //높이 가속

    private bool isJump = true; //점프 중인지 확인하는 사인

    private float mouseX; //마우스 X축

    private float mouseY; //마우스 Y축

    public float rotSpeed;//카메라 회전 속도

    public Transform cam; //카메라

    public float moveSpeed = 10; //이동 속도

    public CharacterController cc; //캐릭터 콘트롤러

    public enum PlayerState

    {

        IDLE, JUMPPOINT

    }

    private void Update()
    {
        CameraRotate(); //카메라 회전 기능
        Move(); //이동 기능
    }

    //카메라 회전 함수

    void CameraRotate()

    {

        float horizontal = Input.GetAxis("Mouse X"); //마우스 가로 이동 입력

        float vertical = Input.GetAxis("Mouse Y"); //마우스 세로 이동 입력

        //마우스 X축의 회전 값을 계산한다.

        mouseX += horizontal * rotSpeed * Time.deltaTime;

        //마우스 Y축의 회전 값을 계산한다.

        mouseY += vertical * rotSpeed * Time.deltaTime;

        //마우스 Y축의 값을 -90에서 90으로 제한한다.

        mouseY = Mathf.Clamp(mouseY, -90.0f, 90.0f);

        //계산된 회전 값 만큼 카메라를 회전시킨다.

        cam.eulerAngles = new Vector3(-mouseY, mouseX, 0);

    }

    //이동 및 점프 함수

    void Move()

    {

        //가로 세로 입력

        float horizontal = Input.GetAxis("Horizontal"); //가로 이동 입력

        float vertical = Input.GetAxis("Vertical"); //세로 이동 입력

        //카메라가 바라보는 방향으로 방향 전환

        Vector3 dir = new Vector3(horizontal, 0, vertical) * moveSpeed;

        dir = cam.TransformDirection(dir);

        switch (playerState)

        {

            case PlayerState.IDLE:

                {

                    //캐릭터가 바닥에 있다면

                    if (cc.isGrounded)

                    {

                        yVelocity = 0;

                        //isJump가 false라면

                        if (!isJump)

                        {

                            //isJump를 true로 변경한다.

                            isJump = true;

                        }

                    }

                    //isJump가 true일 때

                    if (isJump)

                    {

                        //점프

                        if (Input.GetButtonDown("Jump"))

                        {

                            yVelocity = jumpPower;

                            //isJump를 false로 변경한다.

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

        //중력 계산

        yVelocity += gravity * Time.deltaTime;

        dir.y = yVelocity;

        //이동

        cc.Move(dir * Time.deltaTime);

    }
}

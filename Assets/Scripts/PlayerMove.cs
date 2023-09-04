using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //캐릭터 움직임
    public float moveSpeed = 10.0f;
    public float jumpPower = 10.0f;
    public float gravityPower = -30.0f;
    public float yV = 0;

    private Rigidbody rb;
    public Transform cameraTransform;
    public CharacterController characterController;

    public static PlayerMove instance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerMove.instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
        PCMove();

    }

    private void LateUpdate()
    {

    }

    void PCMove()
    {
        //수직 수평 값
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0.0f, v);

        movement = cameraTransform.TransformDirection(movement); //월드상 카메라에서 바라보는 방향
        movement *= moveSpeed;

        if (characterController.isGrounded) //접지하고 있는지 판단
        {
            yV = 0;
            if (Input.GetKeyDown(KeyCode.Space)) //접지하고 있지 않으면 작동되지 않음
            {
                yV = jumpPower; //접지함과 동시에 space를 눌렀다면 jumpPower 적용
            }
        }
        else
        {
            yV += (gravityPower * Time.deltaTime); //jumpPower에 gravityPower * deltaTime 적용
        }

        movement.y = yV; //빈 y좌표에 집어넣음
        characterController.Move(movement * Time.deltaTime); //movement에 deltaTime 적용, Vector3에 deltaTime 적용이라봐도 무방할듯
    }

    public void IncreaseY()
    {
        Debug.Log("호출");
	    yV = 50;

    }
}

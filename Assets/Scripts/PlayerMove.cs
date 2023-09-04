using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //ĳ���� ������
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
        //���� ���� ��
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0.0f, v);

        movement = cameraTransform.TransformDirection(movement); //����� ī�޶󿡼� �ٶ󺸴� ����
        movement *= moveSpeed;

        if (characterController.isGrounded) //�����ϰ� �ִ��� �Ǵ�
        {
            yV = 0;
            if (Input.GetKeyDown(KeyCode.Space)) //�����ϰ� ���� ������ �۵����� ����
            {
                yV = jumpPower; //�����԰� ���ÿ� space�� �����ٸ� jumpPower ����
            }
        }
        else
        {
            yV += (gravityPower * Time.deltaTime); //jumpPower�� gravityPower * deltaTime ����
        }

        movement.y = yV; //�� y��ǥ�� �������
        characterController.Move(movement * Time.deltaTime); //movement�� deltaTime ����, Vector3�� deltaTime �����̶���� �����ҵ�
    }

    public void IncreaseY()
    {
        Debug.Log("ȣ��");
	    yV = 50;

    }
}

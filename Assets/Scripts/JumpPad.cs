using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10.0f;
    public float cooldownTime = 2.0f;
    public PlayerMove playerMove;
    private bool canJump = true;

    public Jumpu jumpu;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canJump)
        {
            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null)
            {
                //Vector3 jumpVector = Vector3.up * jumpForce;
                ////characterController.Move(jumpVector);
                //canJump = false;
                //Invoke("ResetJump", cooldownTime);
                //playerMove.IncreaseY();

                PlayerMove.instance.IncreaseY();

                jumpu.playerState = Jumpu.PlayerState.JUMPPOINT;
            }
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }
}

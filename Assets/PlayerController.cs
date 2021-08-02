using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private const float MOVE_SPEED = 5f;
    private const float ROTATION_SPEED = 0.15f;


    private StateHandler stateHandler;
    private Animator playerAnimator;

    private Rigidbody playerRB;
    private Vector3 moveDir;
   
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        stateHandler = gameObject.GetComponent<StateHandler>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerInputs();
        SetPlayerAnim();


    }
    private void FixedUpdate()
    {
        if (stateHandler.playerCurState == StateHandler.PlayerState.Running)
        {
            playerRB.position += moveDir * MOVE_SPEED * Time.deltaTime;
            //transform.rotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), ROTATION_SPEED);
            //playerRB.velocity = moveDir * MOVE_SPEED;
        }     
    }


    private void PlayerInputs()
    {
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");

        moveDir = new Vector3(horInput, 0, verInput).normalized;
        if(moveDir.magnitude != 0)
        {
            stateHandler.playerCurState = StateHandler.PlayerState.Running;
        }
        else
        {
            stateHandler.playerCurState = StateHandler.PlayerState.Idle;
            playerRB.velocity = Vector3.zero;
        }

    }
    private void SetPlayerAnim()
    {
        switch (stateHandler.playerCurState)
        {
            case StateHandler.PlayerState.Idle:
                //playerAnimator.set
                playerAnimator.SetBool("running", false);
                break;
            case StateHandler.PlayerState.Running:
                playerAnimator.SetBool("running", true);
                break;
            case StateHandler.PlayerState.Bath:
                playerAnimator.SetBool("running", false);
                playerAnimator.SetTrigger("bath");
                break;
            case StateHandler.PlayerState.Dead:
                playerAnimator.SetTrigger("dead");
                break;
        }
    } 
}

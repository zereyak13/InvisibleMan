using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHandler : MonoBehaviour
{
    public enum  PlayerState
    {
        Idle,
        Running,
        Bath,
        Dead
    }
    public PlayerState playerCurState;



    private void Start()
    {
        playerCurState = PlayerState.Idle;
    }


    void Update()
    {
        //Debug.Log(PlayerState.Idle);    
    }
    public void SetPlayerState(int StateID)
    {
        switch (StateID)
        {
            case (int)PlayerState.Idle:
                playerCurState = PlayerState.Idle;
                break;
            case (int)PlayerState.Running:
                playerCurState = PlayerState.Running;
                break;
            case (int)PlayerState.Bath:
                playerCurState = PlayerState.Running;
                break;
        }
    }
}

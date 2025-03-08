using UnityEngine;

public class IdleCharacterState : State 
{
    public IdleCharacterState(Player player)
    {
        _owner = player;
    }

    public Player _owner { get; }

    public override void OnCollisionEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnd()
    {
        Debug.Log("Sto uscendo da Idle");
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnStart()
    {
        Debug.Log("Sto entrando in Idle");
    }

    public override void OnTriggerEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        if (_owner.JumpRequested)
        {
            _owner.JumpResponse();
            _owner.SetState(ECharacterState.Jumping);
            return;
        }

        if (_owner.MoveRequest)
        {
            _owner.SetState(ECharacterState.Walking);
            return;
        }

        if (_owner.AttackRequested)
        {
            _owner.SetState(ECharacterState.Attacking);
            return;
        }
    }
}
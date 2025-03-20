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
    }

    public override void OnTriggerExit()
    {
    }

    public override void OnUpdate()
    {
        // Player was moved by an external force and fell
        if (!_owner.IsGrounded)
        {
            _owner.SetState(ECharacterState.Falling);
            return;
        }

        if (_owner.JumpChargeRequested)
        {
            _owner.SetState(ECharacterState.JumpCharging);
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
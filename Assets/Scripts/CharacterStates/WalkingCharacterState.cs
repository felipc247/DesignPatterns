using UnityEngine;

public class WalkingCharacterState : State
{
    public WalkingCharacterState(Player player)
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
        Debug.Log("Sto uscendo da Walking");
    }

    public override void OnFixedUpdate()
    {
        _owner.MoveHorizontal();
    }

    public override void OnStart()
    {
        Debug.Log("Sto entrando in Walking");
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
        if (!_owner.MoveRequest)
        {
            _owner.SetState(ECharacterState.Idle);
            return;
        }

        if (!_owner.IsGrounded)
        {
            _owner.SetState(ECharacterState.Falling);
            return;
        }

        if (_owner.JumpRequested)
        {
            _owner.JumpResponse();
            _owner.SetState(ECharacterState.Jumping);
            return;
        }

        if (_owner.AttackRequested)
        {
            _owner.SetState(ECharacterState.Attacking);
            return;
        }
    }
}

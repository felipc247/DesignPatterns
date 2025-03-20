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
        _owner.Animator.SetBool("Moving", false);
    }

    public override void OnFixedUpdate()
    {
        _owner.MoveHorizontal();
    }

    public override void OnStart()
    {
        Debug.Log("Sto entrando in Walking");
        _owner.Animator.SetBool("Moving", true);
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
        //if(_owner.HitRequested)
        //{
        //    _owner.SetState(ECharacterState.Hit);
        //    return;
        //}

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

        if (_owner.AttackRequested)
        {
            _owner.SetState(ECharacterState.Attacking);
            return;
        }

        if (!_owner.MoveRequest)
        {
            _owner.SetState(ECharacterState.Idle);
            return;
        }
    }
}

using UnityEngine;

public class JumpChargingCharacterState : State
{
    private float _startTime;

    public JumpChargingCharacterState(Player player)
    {
        _owner = player;
    }

    public Player _owner { get; }

    public override void OnCollisionEnter()
    {
    }

    public override void OnCollisionExit()
    {
    }

    public override void OnEnd()
    {
        Debug.Log("Sto uscendo da JumpCharge");
        _owner.Animator.SetBool("JumpCharging", false);
    }

    public override void OnFixedUpdate()
    {
        // No need to check for groundCheck, we already do that on Update
        _owner.MoveHorizontal();
    }

    public override void OnStart()
    {
        _startTime = Time.time;
        Debug.Log("Sto entrando in JumpCharge");

        _owner.Animator.SetBool("JumpCharging", true);
    }

    public override void OnTriggerEnter()
    {
    }

    public override void OnTriggerExit()
    {
    }

    public override void OnUpdate()
    {
        if (!_owner.IsGrounded)
        {
            _owner.SetState(ECharacterState.Falling);
            return;
        }

        if (_owner.JumpRequested)
        {
            Debug.LogWarning("JumpResponse");
            _owner.TimeJumpCharge = Time.time - _startTime;
            _owner.JumpResponse();
            _owner.SetState(ECharacterState.Jumping);
            return;
        }
    }
}

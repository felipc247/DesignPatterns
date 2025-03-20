using UnityEngine;

public class AttackCharacterState : State
{
    private float _delay = 1f;
    private float _timePassed = 0;
    Player _owner;
    public AttackCharacterState(Player player)
    {
        _owner = player;
    }

    public override void OnCollisionEnter()
    {
    }

    public override void OnCollisionExit()
    {
    }

    public override void OnEnd()
    {
        Debug.Log("Sto uscendo da Attack");

        _owner.Animator.SetBool("Attacking", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnStart()
    {
        Debug.Log("Sto entrando in Attack");

        _timePassed = 0;
        _owner.AttackResponse();

        // Changed to bool as triggers accumulated, especially on jump, leading to animations to break
        _owner.Animator.SetBool("Attacking", true);
    }

    public override void OnTriggerEnter()
    {
    }

    public override void OnTriggerExit()
    {
    }

    public override void OnUpdate()
    {
        _timePassed += Time.deltaTime;
        if (_timePassed < _delay)
            return;

        if (!_owner.IsGrounded)
        {
            _owner.SetState(ECharacterState.Falling);
            return;
        }

        if (_owner.MoveRequest)
        {
            _owner.SetState(ECharacterState.Walking);
            return;
        }

        _owner.SetState(ECharacterState.Idle);
    }
}
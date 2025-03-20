using UnityEngine;

public class HitCharacterState : State
{
    private float _delay = 0.517f;
    private float _timePassed = 0;

    public HitCharacterState(Player player)
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
        Debug.Log("Sto uscendo da Hit");
    }

    public override void OnFixedUpdate()
    {
        _owner.MoveHorizontal();
    }

    public override void OnStart()
    {
        Debug.Log("Sto entrando in Hit");
        // By not putting the SetState on each state
        // we don't have the possibility of customizing the behavior
        // for instance there could be an immune state where we don't care if the player is hit

        // a solution to this would be to check here directly
        // switch (_owner.StateMachine.CurrentStateType)
        // {
        //     case ECharacterState.Immune:
        //         _owner.HitResponse();
        //         break;
        // }
        _owner.TakeDamage(_owner.RequestedDamage);
        _owner.HitResponse();
        _owner.Animator.SetTrigger("Hit");    
    }

    public override void OnTriggerEnter()
    {
    }

    public override void OnTriggerExit()
    {
    }

    public override void OnUpdate()
    {
        if(_delay > _timePassed)
        {
            _timePassed += Time.deltaTime;
            return;
        }

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
    }
}

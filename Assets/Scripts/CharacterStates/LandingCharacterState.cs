using UnityEngine;

public class LandingCharacterState : State
{
    private float _delay = 0.25f;
    private float _timePassed = 0;
    public LandingCharacterState(Player player)
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
        Debug.Log("Sto uscendo da Landing");
        _owner.Animator.SetBool("Landing", false);
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnStart()
    {
        _timePassed = 0;
        _owner.Animator.SetBool("Landing", true);
    }

    public override void OnTriggerEnter()
    {
    }

    public override void OnTriggerExit()
    {
    }

    public override void OnUpdate()
    {
        if (_owner.MoveRequest)
        {
            _owner.SetState(ECharacterState.Walking);
            return;
        }

        _timePassed += Time.deltaTime;
        if (_timePassed < _delay)
            return;

        _owner.SetState(ECharacterState.Idle);
    }
}

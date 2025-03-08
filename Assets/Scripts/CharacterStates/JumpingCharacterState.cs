using UnityEngine;

public class JumpingCharacterState : State
{
    private float previousY;
    public JumpingCharacterState(Player player)
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
        Debug.Log("Sto uscendo da Jumping");
    }

    public override void OnFixedUpdate()
    {
        _owner.MoveHorizontal();
    }

    public override void OnStart()
    {
        _owner.JumpImpulse();
        previousY = _owner.transform.position.y;
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
        if (_owner.IsGrounded)
        {
            // esco
            _owner.SetState(ECharacterState.Landing);
            return;
        }

        if (previousY > _owner.transform.position.y)
        {
            // sto cadendo
            _owner.SetState(ECharacterState.Falling);
            return;
        }

        previousY = _owner.transform.position.y;
    }
}

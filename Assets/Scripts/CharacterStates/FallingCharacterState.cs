using UnityEngine;

public class FallingCharacterState : State
{
    public FallingCharacterState(Player player)
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
        Debug.Log("Sto uscendo da Falling");
    }

    public override void OnFixedUpdate()
    {
    }

    public override void OnStart()
    {
        Debug.Log("Sto entrando in Falling");
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
            _owner.SetState(ECharacterState.Landing);
            return;
        }
    }
}

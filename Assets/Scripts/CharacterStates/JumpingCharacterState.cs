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
    }

    public override void OnCollisionExit()
    {
    }

    public override void OnEnd()
    {
        Debug.Log("Sto uscendo da Jumping");
        _owner.Animator.SetBool("Jumping", false);
    }

    public override void OnFixedUpdate()
    {
        _owner.MoveHorizontal();
    }

    private bool startDone = false;

    public override void OnStart()
    {
        startDone = true;
        Debug.Log("Sto entrando in Jumping");
        _owner.JumpResponse();
        _owner.JumpImpulse();

        _owner.Animator.SetBool("Jumping", true);

        previousY = _owner.transform.position.y;
    }

    public override void OnTriggerEnter()
    {
    }

    public override void OnTriggerExit()
    {
    }

    public override void OnUpdate()
    {
        if (!startDone)
        {
            Debug.Log("Jumping: OnStart not done");
            return;
        }   
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

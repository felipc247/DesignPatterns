using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] Transform groundCheckPivot;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckDistance;
    [SerializeField] float jumpForce;

    [SerializeField] float minimumJumpForce;
    [SerializeField] float jumpChargeMaxTime;

    [SerializeField] float movementSpeed;

    [SerializeField] float horizonatlSensitivity;
    [SerializeField] float verticalSensitivity;

    [SerializeField] float hp;
    [SerializeField] float hpMax;

    private Camera mainCamera;

    public Animator Animator;
    Rigidbody _rigidBody;

    public float RequestedDamage;

    public GenericStateMachine<ECharacterState> StateMachine;

    public float TimeJumpCharge { get; set; }

    public bool JumpRequested { get; private set; } = false;
    public bool MoveRequest { get; private set; } = false;
    public bool IsGrounded { get; private set; }
    public Vector2 MoveDirection { get; private set; }
    public bool AttackRequested { get; private set; }
    public bool JumpChargeRequested { get; private set; }
    public bool HitRequested { get; private set; }

    private void Awake()
    {
        hp = hpMax;

        Cursor.lockState = CursorLockMode.Locked;

        Animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody>();

        StateMachine = new GenericStateMachine<ECharacterState>();

        StateMachine.RegisterState(ECharacterState.Idle, new IdleCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Walking, new WalkingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Jumping, new JumpingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Falling, new FallingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Landing, new LandingCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Attacking, new AttackCharacterState(this));
        StateMachine.RegisterState(ECharacterState.Hit, new HitCharacterState(this));
        StateMachine.RegisterState(ECharacterState.JumpCharging, new JumpChargingCharacterState(this));

        SetState(ECharacterState.Idle);

        mainCamera = Camera.main;
    }

    public void GroundCheck()
    {
        IsGrounded = Physics.Raycast(new Ray(groundCheckPivot.position, Vector3.down), groundCheckDistance, groundMask);
    }

    public void SetState(ECharacterState newState)
    {
        StateMachine.SetState(newState);
    }

    void Update()
    {
        GroundCheck();

        StateMachine.OnUpdate();

        // You can be hit in any state
        if (HitRequested)
        {
            SetState(ECharacterState.Hit);
        }

        mainCamera.transform.position = transform.position + new Vector3(0, 2f, -1.5f);
    }

    private void FixedUpdate()
    {
        StateMachine.OnFixedUpdate();
    }

    internal void JumpChargeRequest()
    {
        // We can't jump if we are already in the air
        // also we can't jump if we are not idle or walking, this caused jumpRequested to be true
        // but JumpChargeRequest to be false if we release space before entering one of them,
        // this caused an immediate jump at minimum force the next time that space was pressed in a valid state

        //if (!IsGrounded) return;
        if (StateMachine.CurrentStateType != ECharacterState.Walking && StateMachine.CurrentStateType != ECharacterState.Idle) return;

        JumpChargeRequested = true;
    }

    internal void JumpChargeResponse()
    {
        JumpChargeRequested = false;
        JumpRequest();
    }

    internal void JumpRequest()
    {
        JumpRequested = true;
    }

    internal void JumpResponse()
    {
        JumpRequested = false;
    }

    internal void HitRequest(float damage)
    {
        HitRequested = true;
        RequestedDamage = damage;
    }

    internal void HitResponse()
    {
        HitRequested = false;
    }

    internal void MoveDirectionRequest(Vector2 direction)
    {
        MoveRequest = direction != Vector2.zero;
        MoveDirection = direction;
    }

    internal void JumpImpulse()
    {
        // % of the jump force based on the time the jump button was pressed

        float jumpForcePercentage = TimeJumpCharge / jumpChargeMaxTime;

        if (TimeJumpCharge > jumpChargeMaxTime)
        {
            jumpForcePercentage = 1;
        }

        float finalJumpForce = jumpForcePercentage * jumpForce;
        finalJumpForce = Mathf.Clamp(finalJumpForce, minimumJumpForce, jumpForce);

        _rigidBody.AddForce(new Vector3(0, finalJumpForce, 0), ForceMode.Impulse);
    }

    internal void MoveHorizontal()
    {
        Vector3 horizontalMovement = transform.forward.normalized * MoveDirection.y + transform.right.normalized * MoveDirection.x;

        _rigidBody.linearVelocity =
            new Vector3(horizontalMovement.normalized.x * movementSpeed * Time.fixedDeltaTime, _rigidBody.linearVelocity.y, horizontalMovement.normalized.z * movementSpeed * Time.fixedDeltaTime);
    }

    internal void AttackRequest()
    {
        AttackRequested = true;
    }

    internal void AttackResponse()
    {
        AttackRequested = false;
    }

    public void TakeDamage(float damage)
    {
        if (!HitRequested)
        {
            // to use TakeDamage and call it from the Hit state
            HitRequest(damage);
            // Quick solution, directly set from here so we don't have to write it on every state
            StateMachine.SetState(ECharacterState.Hit);
            return;
        }

        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Player died");
    }

    private float horizontallRotation;
    private float verticalRotation;

    internal void Look(Vector2 vector2)
    {
        horizontallRotation += vector2.x * horizonatlSensitivity * Time.smoothDeltaTime;
        verticalRotation += vector2.y * verticalSensitivity * Time.smoothDeltaTime;

        // Clamp vertical rotation to prevent camera flipping
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(-verticalRotation, horizontallRotation, 0);
        transform.rotation = Quaternion.Euler(0, mainCamera.transform.localRotation.eulerAngles.y, 0);
    }
}

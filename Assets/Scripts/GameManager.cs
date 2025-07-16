using DesignPatterns.Generics;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Player player;
    public Transform FoodSpawnPoint;
    public IFactory FoodFactory { get; private set; }

    InputSystem_Actions _input;

    public override void Awake()
    {
        base.Awake();
        FoodFactory = new Factory();

        var spawnPoint = FindFirstObjectByType<SpawnPoint>();
        if (spawnPoint != null)
        {
            FoodSpawnPoint = spawnPoint.transform;
        }

        _input = new InputSystem_Actions();

        _input.Player.Move.performed += Move_performed;
        _input.Player.Move.canceled += Move_canceled;

        _input.Player.JumpCharge.performed += JumpCharge_performed;
        _input.Player.JumpCharge.canceled += JumpCharge_canceled;

        _input.Player.Attack.performed += Attack_performed;
        _input.Player.Look.performed += Look_performed;

        _input.Enable();
    }

    private void Look_performed(InputAction.CallbackContext context)
    {
        player.Look(context.ReadValue<Vector2>());
    }

    private void JumpCharge_canceled(InputAction.CallbackContext obj)
    {
        // if request went through, respond
        if (player.JumpChargeRequested) player.JumpChargeResponse();
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        player.AttackRequest();
    }

    private void JumpCharge_performed(InputAction.CallbackContext obj)
    {
        player.JumpChargeRequest();
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        player.MoveDirectionRequest(Vector2.zero);
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        player.MoveDirectionRequest(obj.ReadValue<Vector2>());
    }
}
 
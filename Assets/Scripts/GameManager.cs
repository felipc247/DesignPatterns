using DesignPatterns.Generics;
using UnityEngine;

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

        _input.Player.Jump.performed += Jump_performed;

        _input.Player.Attack.performed += Attack_performed;

        _input.Enable();
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.AttackRequest();
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.JumpRequest();
    }

    private void Move_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.MoveDirectionRequest(Vector2.zero);
    }

    private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        player.MoveDirectionRequest(obj.ReadValue<Vector2>());
    }
}
 
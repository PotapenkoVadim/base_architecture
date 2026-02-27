using System.Numerics;
using UnityEngine.InputSystem;
using System.Linq;

public class InputProvider: IGameModule
{
  private readonly InputSystem_Actions _input;
  private readonly EventBus _bus;

  public InputProvider(EventBus bus)
  {
    _bus = bus;
    _input = new();
  }

  public void Initialize()
  {
    _input.Enable(); 
  }

  public Vector2 GetMovementVector()
  {
    return _input.Player.Move.ReadValue<Vector2>();
  }

  public void ToggleInput(bool active)
  {
    if (active) _input.Enable();
    else _input.Disable();
  }

  public bool IsUsingGamepad()
  {
    return _input.devices?.Any(d => d is Gamepad) ?? false;
  }
}
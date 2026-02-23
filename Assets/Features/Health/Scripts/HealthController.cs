public class HealthController: IGameModule
{
  private int _hp = 100;
  private readonly EventBus _bus;

  public HealthController(EventBus bus) =>_bus = bus;

  public void Initilize()
  {
    _bus.SubScribe<AddHealthPointEvent>(HandleAddHealth);
    _bus.SubScribe<SubtractHealthPointEvent>(HandleSubstractHealth);
  }

  private void HandleAddHealth(AddHealthPointEvent e)
  {
    if (_hp < 100) {
      _hp += 1;
      _bus.Raise(new HealthChangedEvent { NewValue = _hp, DamageValue = 1 });
    }
  }

  private void HandleSubstractHealth(SubtractHealthPointEvent e)
  {
    if (_hp > 0) {
      _hp -= 1;
      _bus.Raise(new HealthChangedEvent { NewValue = _hp, DamageValue = -1 });
    }
  }

  public int GetValue() => _hp;
}
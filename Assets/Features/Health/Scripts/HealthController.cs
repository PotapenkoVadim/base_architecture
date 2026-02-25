public class HealthController: IGameModule
{
  private readonly HealthModel _model;
  private readonly EventBus _bus;

  public HealthController(EventBus bus, HealthModel model) {
    _bus = bus;
    _model = model;
  }

  public void Initilize()
  {
    _bus.SubScribe<AddHealthPointEvent>(HandleAddHealth);
    _bus.SubScribe<SubtractHealthPointEvent>(HandleSubstractHealth);
  }

  private void HandleAddHealth(AddHealthPointEvent e)
  {
    if (_model.IsLessThanMax()) {
      _model.CurrentHealht += 1;
      _bus.Raise(new HealthChangedEvent { Type = HealthChangeType.Heal });
    }
  }

  private void HandleSubstractHealth(SubtractHealthPointEvent e)
  {
    if (_model.IsMoreThanMin()) {
      _model.CurrentHealht -= 1;
      _bus.Raise(new HealthChangedEvent { Type = HealthChangeType.Damage });
    }
  }
}
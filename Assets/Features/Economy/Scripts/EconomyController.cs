public class EconomyController: IGameModule
{
  private int _amount = 0;
  private readonly EventBus _bus;

  public EconomyController(EventBus bus) =>_bus = bus;

  public void Initilize()
  {
    _bus.SubScribe<AddGoldPointEvent>(HandleAddGold);
    _bus.SubScribe<SubtractGoldPointEvent>(HandleSubtractGold);
  }

  private void HandleAddGold(AddGoldPointEvent e)
  {
    if (_amount < 999)
    {
      _amount += 1;
      _bus.Raise(new GoldChangedEvent() {ChangedValue = 1, NewValue = _amount});
    }
  }

  private void HandleSubtractGold(SubtractGoldPointEvent e)
  {
    if (_amount > 0)
    {
      _amount -= 1;
      _bus.Raise(new GoldChangedEvent() {ChangedValue = 1, NewValue = _amount});
    }
  }

  public int GetAmount() => _amount;
}
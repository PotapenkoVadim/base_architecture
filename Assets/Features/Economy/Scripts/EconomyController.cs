public class EconomyController: IGameModule
{
  private readonly EconomyModel _model;
  private readonly EventBus _bus;

  public EconomyController(EventBus bus, EconomyModel model) {
    _bus = bus;
    _model = model;
  }

  public void Initilize()
  {
    _bus.SubScribe<AddGoldPointEvent>(HandleAddGold);
    _bus.SubScribe<SubtractGoldPointEvent>(HandleSubtractGold);
  }

  private void HandleAddGold(AddGoldPointEvent e)
  {
    if (_model.IsLessThanMax())
    {
      _model.Amount += 1;
      _bus.Raise(new GoldChangedEvent() {Type = GoldChangedType.Profit});
    }
  }

  private void HandleSubtractGold(SubtractGoldPointEvent e)
  {
    if (_model.IsMoreThanMin())
    {
      _model.Amount -= 1;
      _bus.Raise(new GoldChangedEvent() {Type = GoldChangedType.Loss});
    }
  }
}
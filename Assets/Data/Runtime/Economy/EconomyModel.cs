public class EconomyModel: IGameModule
{
  public const int MAX_VALUE = 999;
  public const int MIN_VALUE = 0;

  public int Amount {get; set;} = MIN_VALUE;

  public bool IsValidValue() => Amount >= MIN_VALUE && Amount <= MAX_VALUE;
  public bool IsLessThanMax() => Amount < MAX_VALUE;
  public bool IsMoreThanMin() => Amount > MIN_VALUE;
}
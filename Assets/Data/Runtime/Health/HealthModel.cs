public class HealthModel: IGameModule
{
  public const int MAX_HEALTH = 100;
  public const int MIN_HEALTH = 0;

  public int CurrentHealht {get; set;} = MAX_HEALTH;

  public bool IsValidHealth() => CurrentHealht >= MIN_HEALTH && CurrentHealht <= MAX_HEALTH;
  public bool IsLessThanMax() => CurrentHealht < MAX_HEALTH;
  public bool IsMoreThanMin() => CurrentHealht > MIN_HEALTH;
}
public class SettingsModel: IGameModule
{
  public const float MAX_VOLUME = 1f;
  public const float MIN_VOLUME = 0f;

  public float MasterVolume {get; set;} = MIN_VOLUME;

  public bool IsTurnOff() => MasterVolume == MIN_VOLUME;
}
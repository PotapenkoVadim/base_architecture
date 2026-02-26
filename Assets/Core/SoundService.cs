using UnityEngine;

public class SoundService: IGameModule
{
  private readonly EventBus _bus;
  private readonly SoundConfig _config;
  private readonly AudioSource _audioSource;

  public SoundService(EventBus bus, SoundConfig config, AudioSource audioSource)
  {
    _bus = bus;
    _config = config;
    _audioSource = audioSource;

    _audioSource.spatialBlend = 0f; 
    _audioSource.playOnAwake = false;
    _audioSource.loop = false;
  }

  public void Initilize()
  {
    _bus.SubScribe<GoldChangedEvent>(OnGoldChangedEvent);
    _bus.SubScribe<HealthChangedEvent>(OnHealthChangedEvent);
  }

  private void OnGoldChangedEvent(GoldChangedEvent e)
  {
    if (_config.goldClink != null)
      _audioSource.PlayOneShot(_config.goldClink);
  }

  private void OnHealthChangedEvent(HealthChangedEvent e)
  {
    if (e.Type == HealthChangeType.Heal && _config?.playerHeal != null)
      _audioSource.PlayOneShot(_config.playerHeal);

    else if (e.Type == HealthChangeType.Damage && _config?.playerDamage != null)
      _audioSource.PlayOneShot(_config.playerDamage);
  }
}
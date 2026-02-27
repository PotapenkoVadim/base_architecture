public class SettingsController: IGameModule
{
  private readonly EventBus _bus;
  private readonly SettingsModel _model;

  public SettingsController(EventBus bus, SettingsModel model)
  {
    _bus = bus;
    _model = model;
  }

  public void Initialize()
  {
    _bus.SubScribe<SettingChangedEvent>(OnSettingsChanged);
    ApplySettings();
  }

  private void OnSettingsChanged(SettingChangedEvent e)
  {
    _model.MasterVolume = e.NewVolume;
    ApplySettings();
  }

  private void ApplySettings()
  {
    Services.Get<SoundService>().SetVolume(_model.MasterVolume);
    Services.Get<PersistenceManager>().SaveAll();
  }
}
public class PersistenceManager: IGameModule
{
  private readonly ISaveService _saveService;
  private readonly EconomyModel _economy;
  private readonly HealthModel _health;
  private readonly SettingsModel _settings;

  private const string ECONOMY_KEY = "economy_key";
  private const string HEALTH_KEY = "health_key";
  private const string SETTINGS_KEY = "settings_key";

  public PersistenceManager(
    ISaveService saveService,
    EconomyModel economy,
    HealthModel health,
    SettingsModel settings
  ) {
    _saveService = saveService;
    _economy = economy;
    _health = health;
    _settings = settings;
  }

  public void Initialize() => LoadAll();

  public void SaveAll()
  {
    var economyData = new EconomySaveDTO {Amount = _economy.Amount};
    _saveService.Save(ECONOMY_KEY, economyData);

    var healthData = new HealthSaveDTO {CurrentHealht = _health.CurrentHealht};
    _saveService.Save(HEALTH_KEY, healthData);

    var settingsData = new SettingsSaveDTO {MasterVolume = _settings.MasterVolume};
    _saveService.Save(SETTINGS_KEY, settingsData);
  }

  private void LoadAll()
  {
    var defaultEconomyData = new EconomySaveDTO {Amount = 0};
    var loadedEconomy = _saveService.Load(ECONOMY_KEY, defaultEconomyData);
    _economy.Amount = loadedEconomy.Amount;

    var defaultHealthData = new HealthSaveDTO {CurrentHealht = 100};
    var loadedHealth = _saveService.Load(HEALTH_KEY, defaultHealthData);
    _health.CurrentHealht = loadedHealth.CurrentHealht;

    var defaultSettingsData = new SettingsSaveDTO {MasterVolume = 0.01f};
    var loadedSettings = _saveService.Load(SETTINGS_KEY, defaultSettingsData);
    _settings.MasterVolume = loadedSettings.MasterVolume;
  }
}
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrapper
{
  private const string GLOBAL_CONFIG = "GlobalConfig";

  private static readonly List<object> _activeSystems = new();


  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  public static void Execute()
  {
    var config = Resources.Load<GlobalConfig>(GLOBAL_CONFIG);
    var eventBus = Register(new EventBus());
    Register(new InputProvider(eventBus));
    Register(new SceneService(eventBus));

    #if UNITY_WEBGL
      var saveService = new PlayerPrefsSaveService();
    #else
      var saveService = new JsonSaveService();
    #endif
    Register(saveService);

    var audioConfig = config.audioConfig; 
    var audioSource = CreateGlobalAudioSource();
    Register(new SoundService(eventBus, audioConfig, audioSource));

    var healthModel = Register(new HealthModel());
    var economyModel = Register(new EconomyModel());

    Register(new PersistenceManager(saveService, economyModel, healthModel));

    Register(new HealthController(eventBus, healthModel));
    Register(new EconomyController(eventBus, economyModel));

    InitializeServices();
    InitializeInfrastructure(config);
  }

  private static void InitializeInfrastructure(GlobalConfig config)
  {
    if (config.sceneLoaderPrefab != null)
    {
      var overlay = Object.Instantiate(config.sceneLoaderPrefab);
      Object.DontDestroyOnLoad(overlay);
    }
  }

  private static void InitializeServices()
  {
    foreach (var system in _activeSystems)
    {
      if (system is IGameModule gameModule)
        gameModule.Initialize();
    }
  }

  private static T Register<T>(T system) where T:IGameModule
  {
    _activeSystems.Add(system);
    Services.Register(system);

    return system;
  }

  private static AudioSource CreateGlobalAudioSource()
  {
    var gameObject = new GameObject("GlobalAudioSource");
    Object.DontDestroyOnLoad(gameObject);

    return gameObject.AddComponent<AudioSource>();
  }
}
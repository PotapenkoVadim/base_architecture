using UnityEngine;

public class GameBootstrapper
{
  private const string GLOBAL_CONFIG = "GlobalConfig";

  [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
  public static void Execute()
  {
    var config = Resources.Load<GlobalConfig>(GLOBAL_CONFIG);
    Debug.Log(config);

    var eventBus = new EventBus();
    Services.Register(eventBus);

    var input = new InputProvider(eventBus);
    Services.Register(input);

    var sceneService = new SceneService(eventBus);
    Services.Register(sceneService);

    var healthService = new HealthController(eventBus);
    Services.Register(healthService);

    var economyService = new EconomyController(eventBus);
    Services.Register(economyService);

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
}
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
    input.Initilize();
    Services.Register(input);

    var sceneService = new SceneService(eventBus);
    Services.Register(sceneService);

    InitializeInfrastructure(config);
  }

  private static void InitializeInfrastructure(GlobalConfig config)
  {
    if (config.sceneLoaderPrefab != null)
    {
      var overlay = Object.Instantiate(config.sceneLoaderPrefab);
      Object.DontDestroyOnLoad(overlay.gameObject);
    }
  }
}
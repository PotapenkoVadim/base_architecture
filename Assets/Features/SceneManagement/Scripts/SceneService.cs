using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneService: IGameModule
{
  private readonly EventBus _bus;

  public SceneService(EventBus bus) => _bus = bus;

  public async void LoadScene(SceneData sceneData)
  {
    _bus.Raise(new SceneLoadStartedEvent { SceneName = sceneData.displayName });
    var operation = SceneManager.LoadSceneAsync(sceneData.sceneName);

    while (!operation.isDone)
      await Task.Yield();

    _bus.Raise(new SceneLoadCompletedEvent());
  }
}
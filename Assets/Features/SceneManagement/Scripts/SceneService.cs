using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService
{
  private const float MAX_DELAY = 2.0f;
  private const float AWAITED_PROGRESS = 0.9f;

  private readonly EventBus _bus;

  public SceneService(EventBus bus) => _bus = bus;

  public async void LoadScene(SceneData sceneData)
  {
    _bus.Raise(new SceneLoadStartedEvent { SceneName = sceneData.displayName });

    float startTime = Time.time;
    var operation = SceneManager.LoadSceneAsync(sceneData.sceneName);
    operation.allowSceneActivation = false;

    while (operation.progress < AWAITED_PROGRESS) {
      await Task.Yield();
    }

    float elapsed = Time.time - startTime;
    int remainingDelay = (int)Mathf.Max(0, (MAX_DELAY - elapsed) * 1000);
    if (remainingDelay > 0) await Task.Delay(remainingDelay);

    operation.allowSceneActivation = true;

    while (!operation.isDone) {
      await Task.Yield();
    }

    _bus.Raise(new SceneLoadCompletedEvent());
  }
}
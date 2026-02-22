using UnityEngine;

public class MainMenuView: MonoBehaviour
{
  [SerializeField] private SceneData _gameplayScene;

  private void OnEnable()
  {
    var bus = Services.Get<EventBus>();
    bus.SubScribe<SceneLoadStartedEvent>(OnLoadStarted);
  }

  private void OnDisable()
  {
    var bus = Services.Get<EventBus>();
    bus.Unsubscribe<SceneLoadStartedEvent>(OnLoadStarted);
  }

  public void OnStartClick() => Services.Get<SceneService>().LoadScene(_gameplayScene);

  public void OnOptionsClick() => Debug.Log("Settings will be here");
  public void OnExitClick() => Application.Quit();

  private void OnLoadStarted(SceneLoadStartedEvent e)
  {
    gameObject.SetActive(false);
  }
}
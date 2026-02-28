using UnityEngine;

public class MainMenuView: MonoBehaviour
{
  [SerializeField] private SceneData _gameplayScene;

  public void OnStartClick() => Services.Get<SceneService>().LoadScene(_gameplayScene);

  public void OnOptionsClick() => Services.Get<EventBus>().Raise(new ToggleSettingsViewEvent());
  public void OnExitClick()
  {
    Application.Quit();

    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #endif
  }
}
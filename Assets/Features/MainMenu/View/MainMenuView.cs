using UnityEngine;

public class MainMenuView: MonoBehaviour
{
  [SerializeField] private SceneData _gameplayScene;

  public void OnStartClick() => Services.Get<SceneService>().LoadScene(_gameplayScene);

  public void OnOptionsClick() => Debug.Log("Settings will be here");
  public void OnExitClick() => Application.Quit();
}
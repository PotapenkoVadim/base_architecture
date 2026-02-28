using UnityEngine;
using UnityEngine.UI;

public class LevelSwitcherViews: MonoBehaviour
{
  [SerializeField] private Button _mainMenuButton;
  [SerializeField] private Button _nextLevelButton;
  [SerializeField] private SceneData _mainMenuScene;
  [SerializeField] private SceneData _nextScene;

  private void OnEnable()
  {
    _mainMenuButton.onClick.AddListener(OnMainMenu);
    _nextLevelButton.onClick.AddListener(OnNextLevel);
  }

  private void OnDisable()
  {
    _mainMenuButton.onClick.RemoveListener(OnMainMenu);
    _nextLevelButton.onClick.RemoveListener(OnNextLevel);
  }

  public void OnMainMenu() => Services.Get<SceneService>().LoadScene(_mainMenuScene);
  public void OnNextLevel() => Services.Get<SceneService>().LoadScene(_nextScene);
}
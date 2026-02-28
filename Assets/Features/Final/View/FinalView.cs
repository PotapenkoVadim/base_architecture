using UnityEngine;
using UnityEngine.UI;

public class FinalView: MonoBehaviour
{
  [SerializeField] Button _mainMenuButton;
  [SerializeField] SceneData _mainMenuScene;

  private void OnEnable()
  {
    _mainMenuButton.onClick.AddListener(OnMainMenuClick);
  }

  private void OnDisable()
  {
    _mainMenuButton.onClick.RemoveListener(OnMainMenuClick);
  }

  private void OnMainMenuClick()
  {
    Services.Get<SceneService>().LoadScene(_mainMenuScene);
  }
}
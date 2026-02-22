using TMPro;
using UnityEngine;

public class SceneLoaderView: MonoBehaviour
{
  [SerializeField] private GameObject _loadingOverlay;
  [SerializeField] private TextMeshProUGUI _loadingText;

  private void OnEnable()
  {
    var bus = Services.Get<EventBus>();
    bus.SubScribe<SceneLoadStartedEvent>(OnLoadStarted);
    bus.SubScribe<SceneLoadCompletedEvent>(OnLoadComleted);

    _loadingOverlay.SetActive(false);
  }

  private void OnDisable()
  {
    var bus = Services.Get<EventBus>();
    bus.Unsubscribe<SceneLoadStartedEvent>(OnLoadStarted);
    bus.Unsubscribe<SceneLoadCompletedEvent>(OnLoadComleted);
  }

  private void OnLoadStarted(SceneLoadStartedEvent e)
  {
    _loadingText.text = $"Loading {e.SceneName} scene...";
    _loadingOverlay.SetActive(true);
  }

  private void OnLoadComleted(SceneLoadCompletedEvent e)
  {
    _loadingOverlay.SetActive(false);
  }
}
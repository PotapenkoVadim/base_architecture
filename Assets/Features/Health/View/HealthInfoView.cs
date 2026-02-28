using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthInfoView : MonoBehaviour
{
  private const string START_TEXT = "HEALTH: ";

  [SerializeField] private Image _background;
  [SerializeField] private TextMeshProUGUI _text;
  [SerializeField] private Color _healColor = new(0, 1, 0, 0.5f);
  [SerializeField] private Color _damageColor = new(1, 0, 0, 0.5f);

  private Color _originalColor;
  private CancellationTokenSource _cts;

  private void Awake()
  {
    var healthModel = Services.Get<HealthModel>();
    _text.text = START_TEXT + healthModel.CurrentHealht;
    _originalColor = _background.color;
  }

  private void OnEnable()
  {
    Services.Get<EventBus>().SubScribe<HealthChangedEvent>(UpdateUI);
  }

  private void OnDisable()
  {
    StopCurrentFlash();
    Services.Get<EventBus>().Unsubscribe<HealthChangedEvent>(UpdateUI);
  }

  private void UpdateUI(HealthChangedEvent e)
  {
    _text.text = START_TEXT + Services.Get<HealthModel>().CurrentHealht;

    Color targetColor = e.Type == HealthChangeType.Heal ? _healColor : _damageColor;

    StopCurrentFlash();
    _cts = new CancellationTokenSource();

    FlashBackgroundAsync(targetColor, _cts.Token);
  }

  private async void FlashBackgroundAsync(Color targetColor, CancellationToken token)
  {
    try
    {
      _background.color = targetColor;

      float pauseEndTime = Time.unscaledTime + 0.2f;
      while (Time.unscaledTime < pauseEndTime)
      {
        await Task.Yield();
        token.ThrowIfCancellationRequested();
      }

      float duration = 0.3f;
      float elapsed = 0;

      while (elapsed < duration)
      {
        await Task.Yield();
        token.ThrowIfCancellationRequested();

        elapsed += Time.unscaledDeltaTime;
        float t = Mathf.Clamp01(elapsed / duration);
        
        _background.color = Color.Lerp(targetColor, _originalColor, t);
      }

      _background.color = _originalColor;
    }
    catch (Exception)
    {
      _background.color = _originalColor;
    }
  }

  private void StopCurrentFlash()
  {
    if (_cts != null)
    {
      _cts.Cancel();
      _cts.Dispose();
      _cts = null;
    }
  }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldInfoView : MonoBehaviour
{
  private const string START_TEXT = "GOLD: ";

  [SerializeField] private Image _background;
  [SerializeField] private TextMeshProUGUI _text;
  [SerializeField] private Color _eventColor = new(1f, 0.85f, 0f, 0.5f);

  private Color _originalColor;
  private CancellationTokenSource _cts;

  private void Awake()
  {
    var initialGold = Services.Get<EconomyModel>().Amount;
    _text.text = START_TEXT + initialGold;
    _originalColor = _background.color;
  }

  private void OnEnable()
  {
    Services.Get<EventBus>().SubScribe<GoldChangedEvent>(UpdateUI);
  }

  private void OnDisable()
  {
    StopCurrentFlash();
    Services.Get<EventBus>().Unsubscribe<GoldChangedEvent>(UpdateUI);
  }

  private void UpdateUI(GoldChangedEvent e)
  {
    _text.text = START_TEXT + Services.Get<EconomyModel>().Amount;

    StopCurrentFlash();
    _cts = new CancellationTokenSource();

    FlashBackgroundAsync(_cts.Token);
  }

  private async void FlashBackgroundAsync(CancellationToken token)
  {
    try
    {
      _background.color = _eventColor;

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
        
        _background.color = Color.Lerp(_eventColor, _originalColor, t);
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

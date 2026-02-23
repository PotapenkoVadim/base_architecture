using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthInfoView: MonoBehaviour
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
    var initialHP = Services.Get<HealthController>().GetValue();
    _text.text = START_TEXT + initialHP;

    _originalColor = _background.color;
  }

  private void OnEnable() {
    Services.Get<EventBus>().SubScribe<HealthChangedEvent>(UpdateUI);
  }

  private void OnDisable() {
    _cts?.Cancel();
    Services.Get<EventBus>().Unsubscribe<HealthChangedEvent>(UpdateUI);
  }

  private void UpdateUI(HealthChangedEvent e)
  {
    Color targetColor = e.DamageValue > 0 ? _healColor : _damageColor;

    _cts?.Cancel();
    _cts = new CancellationTokenSource();

    _text.text = START_TEXT + e.NewValue;

    FlashBackgroundAsync(targetColor, _cts.Token);
  }

  private async void FlashBackgroundAsync(Color targetColor, CancellationToken token)
  {
    try
    {
      _background.color = targetColor;
      await Task.Delay(200, token);

      float duration = 0.3f;
      float elapsed = 0;

      while (elapsed < duration)
      {
        token.ThrowIfCancellationRequested();
        elapsed += Time.deltaTime;
        _background.color = Color.Lerp(targetColor, _originalColor, elapsed / duration);

        await Task.Yield();
      }

      _background.color = _originalColor;
    } catch (System.OperationCanceledException)
    {
      _background.color = _originalColor;
    }
  }
}
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldInfoView: MonoBehaviour
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

  private void OnEnable() {
    Services.Get<EventBus>().SubScribe<GoldChangedEvent>(UpdateUI);
  }

  private void OnDisable() {
    _cts?.Cancel();
    Services.Get<EventBus>().Unsubscribe<GoldChangedEvent>(UpdateUI);
  }

  private void UpdateUI(GoldChangedEvent e)
  {
    _cts?.Cancel();
    _cts = new CancellationTokenSource();

    _text.text = START_TEXT + Services.Get<EconomyModel>().Amount;

    FlashBackgroundAsync(_cts.Token);
  }

  private async void FlashBackgroundAsync(CancellationToken token)
  {
    try
    {
      _background.color = _eventColor;
      await Task.Delay(200, token);

      float duration = 0.3f;
      float elapsed = 0;

      while (elapsed < duration)
      {
        token.ThrowIfCancellationRequested();
        elapsed += Time.deltaTime;
        _background.color = Color.Lerp(_eventColor, _originalColor, elapsed / duration);

        await Task.Yield();
      }

      _background.color = _originalColor;
    } catch (System.OperationCanceledException)
    {
      _background.color = _originalColor;
    }
  }
}
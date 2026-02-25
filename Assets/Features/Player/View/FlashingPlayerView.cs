using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class FlashingPlayerView : MonoBehaviour
{
  [SerializeField] private Color _goldEventColor = new(1f, 0.85f, 0f, 0.5f);
  [SerializeField] private Color _healColor = new(0, 1, 0, 0.5f);
  [SerializeField] private Color _damageColor = new(1, 0, 0, 0.5f);
  [SerializeField] private Renderer _renderer;

  private Color _originalColor;
  private MaterialPropertyBlock _propBlock;
  private CancellationTokenSource _cts;
  private static readonly int ColorProperty = Shader.PropertyToID("_BaseColor");

  private void Awake()
  {
    _propBlock = new MaterialPropertyBlock();
    if (_renderer != null)
      _originalColor = _renderer.sharedMaterial.color;
  }

  private void OnEnable()
  {
    var eventBus = Services.Get<EventBus>();
    eventBus.SubScribe<HealthChangedEvent>(HandleHealthChanged);
    eventBus.SubScribe<GoldChangedEvent>(HandleGoldChanged);
  }

  private void OnDisable()
  {
    StopFlash();
    var eventBus = Services.Get<EventBus>();
    eventBus.Unsubscribe<HealthChangedEvent>(HandleHealthChanged);
    eventBus.Unsubscribe<GoldChangedEvent>(HandleGoldChanged);
  }

  private void HandleHealthChanged(HealthChangedEvent e)
  {
    Color targetColor = e.Type == HealthChangeType.Heal ? _healColor : _damageColor;
    StartFlash(targetColor);
  }

  private void HandleGoldChanged(GoldChangedEvent e)
  {
    StartFlash(_goldEventColor);
  }

  private void StartFlash(Color color)
  {
    StopFlash();
    _cts = new CancellationTokenSource();
    FlashAsync(color, _cts.Token);
  }

  private void StopFlash()
  {
    _cts?.Cancel();
    _cts?.Dispose();
    _cts = null;
  }

  private async void FlashAsync(Color targetColor, CancellationToken token)
  {
    try
    {
      ApplyColor(targetColor);
      await Task.Delay(150, token);

      float duration = 0.25f;
      float elapsed = 0;

      while (elapsed < duration)
      {
        elapsed += Time.deltaTime;
        Color lerpedColor = Color.Lerp(targetColor, _originalColor, elapsed / duration);
        ApplyColor(lerpedColor);

        await Task.Yield();
        token.ThrowIfCancellationRequested();
      }

      ApplyColor(_originalColor);
    }
    catch (OperationCanceledException)
    {
      ApplyColor(_originalColor);
    }
  }

  private void ApplyColor(Color color)
  {
    if (_renderer == null) return;
    _renderer.GetPropertyBlock(_propBlock);
    _propBlock.SetColor(ColorProperty, color);
    _renderer.SetPropertyBlock(_propBlock);
  }
}

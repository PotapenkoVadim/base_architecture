using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView: MonoBehaviour
{
  [SerializeField] private GameObject _overlay;
  [SerializeField] private Slider _slider;
  [SerializeField] private Button _saveButton;
  [SerializeField] private Button _cancelButton;
  [SerializeField] private TextMeshProUGUI _volumeValueText;

  private void Awake() => UpdateUI();

  private void OnEnable()
  {
    _overlay.SetActive(false);
    Services.Get<EventBus>().SubScribe<ToggleSettingsViewEvent>(OnToggleSettingsView);
  }

  private void OnDisable()
  {
    Services.Get<EventBus>().Unsubscribe<ToggleSettingsViewEvent>(OnToggleSettingsView);
  }

  public void OnChangeSlider(float value)
  {
    Services.Get<EventBus>().Raise(new SettingChangedEvent {NewVolume = value});
    UpdateUI(value);
  }

  public void OnSave() {
    Services.Get<PersistenceManager>().SaveAll();
    _overlay.SetActive(false);
  }

  public void OnCancel() => _overlay.SetActive(false);

  private void UpdateUI()
  {
    float initialValue = Services.Get<SettingsModel>().MasterVolume;
    _slider.value = Services.Get<SettingsModel>().MasterVolume;
    _volumeValueText.text = GetDisplayedVolume(initialValue);
  }

  private void UpdateUI(float value)
  {
    _slider.value = value;
    _volumeValueText.text = GetDisplayedVolume(value);
  }

  private string GetDisplayedVolume(float value) => Mathf.RoundToInt(value * 100).ToString();
  private void OnToggleSettingsView(ToggleSettingsViewEvent e) => _overlay.SetActive(!_overlay.activeSelf);
}
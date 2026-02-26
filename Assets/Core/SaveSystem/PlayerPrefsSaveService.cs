using UnityEngine;

public class PlayerPrefsSaveService: ISaveService
{
  public void Save<T>(string key, T data)
  {
    string json = JsonUtility.ToJson(data);
    PlayerPrefs.SetString(key, json);
    PlayerPrefs.Save();
  }

  public T Load<T>(string key, T defaultValue = default)
  {
    if (!PlayerPrefs.HasKey(key)) return defaultValue;

    string json = PlayerPrefs.GetString(key);

    return JsonUtility.FromJson<T>(json);
  }

  public bool HasKey(string key) => PlayerPrefs.HasKey(key);
  public void DeleteKey(string key) => PlayerPrefs.DeleteKey(key);
}
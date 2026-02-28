using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Data/GlobalConfig")]
public class GlobalConfig: ScriptableObject
{
  [Header("Infrastructure")]
  public GameObject sceneLoaderPrefab;
  public GameObject settingsUIPrefab;
  public SceneData StartupScene;

  [Header("Audio Settings")]
  public SoundConfig audioConfig;
}
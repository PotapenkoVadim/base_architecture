using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Data/GlobalConfig")]
public class GlobalConfig: ScriptableObject
{
  [Header("Infrastructure")]
  public GameObject sceneLoaderPrefab;
  public SceneData StartupScene;

  [Header("Audio Settings")]
  public SoundConfig audioConfig;
}
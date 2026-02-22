using UnityEngine;

[CreateAssetMenu(fileName = "GlobalConfig", menuName = "Data/GlobalConfig")]
public class GlobalConfig: ScriptableObject
{
  public GameObject sceneLoaderPrefab;
  public SceneData StartupScene;
}
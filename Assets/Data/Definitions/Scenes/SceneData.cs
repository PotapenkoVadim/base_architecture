using UnityEngine;

[CreateAssetMenu(fileName = "NewScene", menuName = "Data/SceneData")]
public class SceneData: ScriptableObject
{
  public string sceneName;
  public string displayName;
}
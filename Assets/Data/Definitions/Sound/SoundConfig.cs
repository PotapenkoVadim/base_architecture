using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Data/SoundConfig")]
public class SoundConfig: ScriptableObject
{
  public AudioClip playerDamage;
  public AudioClip playerHeal;
  public AudioClip goldClink;
  [Range(0, 1)] public float masterVolume = 1f;
}
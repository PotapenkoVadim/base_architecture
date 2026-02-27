using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Data/SoundConfig")]
public class SoundConfig: ScriptableObject
{
  public AudioClip playerDamage;
  public AudioClip playerHeal;
  public AudioClip goldClink;
}
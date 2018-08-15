using UnityEngine;
using System.Collections;

/// <summary>
/// Створення екземпляра звуків з коду 
/// </summary>
public class SoundEffectsHelper : MonoBehaviour
{

  /// <summary>
  /// Singleton
  /// </summary>
  public static SoundEffectsHelper Instance;

  public AudioClip explosionSound;
  public AudioClip playerShotSound;
  public AudioClip enemyShotSound;

  void Awake()
  {
        // Реєстрація  singleton
        if (Instance != null)
    {
      Debug.LogError("Multiple instances of SoundEffectsHelper!");
    }
    Instance = this;
  }

  public void MakeExplosionSound()
  {
    MakeSound(explosionSound);
  }

  public void MakePlayerShotSound()
  {
    MakeSound(playerShotSound);
  }

  public void MakeEnemyShotSound()
  {
    MakeSound(enemyShotSound);
  }

    /// <summary>
    /// Відтворити певний звук
    /// </summary>
    /// <param name="originalClip"></param>
    private void MakeSound(AudioClip originalClip)
  {
        // Як це не 3D-аудіо кліп, позиція не має значення.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
  }
}

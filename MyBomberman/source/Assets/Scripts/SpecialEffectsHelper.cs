using UnityEngine;

/// <summary>
/// Створення екземпляра частинок з коду
/// </summary>
public class SpecialEffectsHelper : MonoBehaviour
{
  /// <summary>
  /// Singleton
  /// </summary>
  public static SpecialEffectsHelper Instance;

  public ParticleSystem smokeEffect;
  public ParticleSystem fireEffect;

  void Awake()
  {
    // запис singleton
    if (Instance != null)
    {
      Debug.LogError("Multiple instances of SpecialEffectsHelper!");
    }
    Instance = this;
  }

    /// <summary>
    /// Створення вибуху в даному місці
    /// </summary>
    /// <param name="position"></param>
    public void Explosion(Vector3 position)
  {
        // Дим на воді
        instantiate(smokeEffect, position);

        // Ту Ту Ту, Ту Ту Туду

        // Пожежа 
        instantiate(fireEffect, position);
    }

    /// <summary>
 
/// ініціалізує систему частинок з префабрики
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
  {
    ParticleSystem newParticleSystem = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;

        // Переконайтеся, що він буде знищений
        Destroy(newParticleSystem.gameObject, newParticleSystem.startLifetime);

    return newParticleSystem;
  }
}
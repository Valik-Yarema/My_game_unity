using UnityEngine;

/// <summary>
/// поведінка снаряда
/// </summary>
public class ShotScript : MonoBehaviour
{


    /// <summary>
    /// Нанесених ушкоджень
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Projectile пошкодження гравця або ворогів?
    /// </summary>
    public bool isEnemyShot = false;

  void Start()
  {
        // 1 - Обмежений час жити, щоб уникнути будь-яких витоків
        Destroy(gameObject, 20); // 20сек
  }
}
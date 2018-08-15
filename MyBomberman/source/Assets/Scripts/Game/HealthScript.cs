using UnityEngine;

/// <summary>
/// підраунок ударів і пошкодження
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Усього hitpoints
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// Усього ударів?
    /// </summary>
    public bool isEnemy = true;
  
    /// <summary>
    /// Наносити збиток і перевірити, чи повинен об'єкт бути знищений
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
  {
    hp -= damageCount;
      // hels = hp;
    if (hp <= 0)
    {
      // Вибух!
      SpecialEffectsHelper.Instance.Explosion(transform.position);
      SoundEffectsHelper.Instance.MakeExplosionSound();

      // смерть!
      Destroy(gameObject);
    }
  }

  void OnTriggerEnter2D(Collider2D otherCollider)
  {
        // Це постріл?
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
    if (shot != null)
    {
            // Уникайте дружнього вогню
            if (shot.isEnemyShot != isEnemy)
      {
        Damage(shot.damage);

                // Знищити постріл
                Destroy(shot.gameObject); // Не забудьте завжди націлювати об'єкт гри, інакше ви просто видалите скрипт
            }
            }
  }
}
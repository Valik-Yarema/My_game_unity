using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// підраунок ударів і пошкодження
/// </summary>
public class BOSS_DEAD : MonoBehaviour
{
    /// <summary>
    /// Усього hitpoints
    /// </summary>
    public int hp = 1;
    public int LEVEL = 4;
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

        if (hp <= 0)
        {
            // Вибух!
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            SoundEffectsHelper.Instance.MakeExplosionSound();

            // смерть!
            Destroy(gameObject);
            ++LEVEL;
            StartLevel1(LEVEL);
          //  SceneManager.LoadScene(2);
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
    public void StartLevel1(int lev)
    {
        // "scene1" - це назва першої сцени, яку ми створили.



        switch (lev)
        {
            case 0: { SceneManager.LoadScene(lev); break; }
            case 1: { SceneManager.LoadScene(lev); break; }
            case 2: { SceneManager.LoadScene(lev); break; }
            case 3: { SceneManager.LoadScene("Victory"); break; }
            case 4: { SceneManager.LoadScene(lev); break; }
            case 5: { SceneManager.LoadScene(lev); break; }
            case 6: { SceneManager.LoadScene(lev); break; }
            case 7: { SceneManager.LoadScene(lev); break; }
            case 8: { SceneManager.LoadScene(lev); break; }
            case 9: { SceneManager.LoadScene(lev); break; }
            case 10: { SceneManager.LoadScene(lev); break; }
        }

    }
}
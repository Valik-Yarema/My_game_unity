using UnityEngine;

/// <summary>
/// Враження загальної поведінки
/// </summary>
public class EnemyScript : MonoBehaviour
{
  private bool hasSpawn;
  private MoveScript moveScript;
  private WeaponScript[] weapons;

  void Awake()
  {
        // Отримати зброю можна лише один раз
        weapons = GetComponentsInChildren<WeaponScript>();

        // Отримати скрипти для відключення, коли не з'являється
        moveScript = GetComponent<MoveScript>();
  }

  void Start()
  {
    hasSpawn = false;

        // Вимкнути все
        // - коллайдер
        GetComponent<Collider2D>().enabled = false;
    // -- рух
    moveScript.enabled = false;
    // -- постріл
    foreach (WeaponScript weapon in weapons)
    {
      weapon.enabled = false;
    }
  }

  void Update()
  {
        // Перевірте, чи виник ворог
        if (hasSpawn == false)
    {
      if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
      {
        Spawn();
      }
    }
    else
    {
      // авто-вогонь
      foreach (WeaponScript weapon in weapons)
      {
        if (weapon != null && weapon.enabled && weapon.CanAttack)
        {
          weapon.Attack(true);
          SoundEffectsHelper.Instance.MakeEnemyShotSound();
        }
      }

            // Поза камерою?
            if (GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
      {
        Destroy(gameObject);
      }
    }
  }

  private void Spawn()
  {
    hasSpawn = true;

        // увімкнути все
        // - колайдер
        GetComponent<Collider2D>().enabled = true;
    // -- рух
    moveScript.enabled = true;
    // -- постріл
    foreach (WeaponScript weapon in weapons)
    {
      weapon.enabled = true;
    }
  }
}
using UnityEngine;

/// <summary>
///Запустіть снаряд
/// </summary>
public class WeaponScript : MonoBehaviour
{
    //--------------------------------
    // 1 - Дизайнерські змінні
    //--------------------------------

    /// <summary>
    /// Проекційне приладдя для стрільби
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Відновлення в секундах між двома пострілами
    /// </summary>
    public float shootingRate = 0.25f;

  //--------------------------------
  // 2 - Cooldown
  //--------------------------------

  private float shootCooldown;

  void Start()
  {
    shootCooldown = 0f;
  }

  void Update()
  {
    if (shootCooldown > 0)
    {
      shootCooldown -= Time.deltaTime;
    }
  }

    //--------------------------------
    // 3 - Shooting з іншого сценарію
    //--------------------------------

    /// <summary>
    /// Створіть новий снаряд, якщо це можливо
    /// </summary>
    public void Attack(bool isEnemy)
  {
    if (CanAttack)
    {
      shootCooldown = shootingRate;

            // Створіть новий снаряд 
            var shotTransform = Instantiate(shotPrefab) as Transform;

            // Призначити позицію
            shotTransform.position = transform.position;

            // це постріл ворога
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
      if (shot != null)
      {
        shot.isEnemyShot = isEnemy;
      }

            // Зробіть, щоб зброя завжди вбивала його
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
      if (move != null)
      {
        move.direction = this.transform.right; // в напрямку 2D-простору - право sprite
            }
    }
  }

    /// <summary>
    /// Чи можна створити новий снаряд?
    /// </summary>
    public bool CanAttack
  {
    get
    {
      return shootCooldown <= 0f;
    }
  }
}
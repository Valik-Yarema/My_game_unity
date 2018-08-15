using UnityEngine;

/// <summary>
///  загальна поведінка
/// </summary>
public class BossScript : MonoBehaviour
{
  private bool hasSpawn;

    //  Компоненти посилань
    private MoveScript moveScript;
  private WeaponScript[] weapons;
  private Animator animator;
  private SpriteRenderer[] renderers;

    // Шаблон боса (бот  AI)
    public float minAttackCooldown = 0.5f;
  public float maxAttackCooldown = 2f;

  private float aiCooldown;
  private bool isAttacking;
  private Vector2 positionTarget;

  void Awake()
  {
        // Отримати зброю можна лише один раз
        weapons = GetComponentsInChildren<WeaponScript>();

        // Отримати скрипти для відключення, коли не з'являється
        moveScript = GetComponent<MoveScript>();

    // Get the animator
    animator = GetComponent<Animator>();

        // Отримайте візуалізатори у нащадків
        renderers = GetComponentsInChildren<SpriteRenderer>();
  }

  void Start()
  {
    hasSpawn = false;

        // Вимкнути все
        // - коллайдер
        GetComponent<Collider2D>().enabled = false;
    // -- Рух
    moveScript.enabled = false;
    // -- Стрільба
    foreach (WeaponScript weapon in weapons)
    {
      weapon.enabled = false;
    }

        //Поведінка за замовчуванням
    isAttacking = false;
    aiCooldown = maxAttackCooldown;
  }

  void Update()
  {
        // Перевірте, чи виник ворог
        if (hasSpawn == false)
    {
            // Ми перевіряємо лише перший візуалізатор для простоти.
            // Але ми не знаємо, чи це тіло, і око, або рот ...
            if (renderers[0].IsVisibleFrom(Camera.main))
      {
        Spawn();
      }
    }
    else
    {
            // AI
            // ------------------------------------
            // Перемістити або атакувати перемотати Повторити
            aiCooldown -= Time.deltaTime;

      if (aiCooldown <= 0f)
      {
        isAttacking = !isAttacking;
        aiCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
        positionTarget = Vector2.zero;

                // Встановити або вимкнути атаку атаки
                animator.SetBool("Attack", isAttacking);
      }

      // Атака
      //----------
      if (isAttacking)
      {
                // Зупиніть будь-який рух
                moveScript.direction = Vector2.zero;

        foreach (WeaponScript weapon in weapons)
        {
          if (weapon != null && weapon.enabled && weapon.CanAttack)
          {
            weapon.Attack(true);
            SoundEffectsHelper.Instance.MakeEnemyShotSound();
          }
        }
      }
      // рух
      //----------
      else
      {
                // Визначте ціль?
                if (positionTarget == Vector2.zero)
        {
                    // Отримання точки на екрані, перетворення в світ
                    Vector2 randomPoint = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

          positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);
        }

                // ми на ціль? Якщо так, знайдіть новий
                if (GetComponent<Collider2D>().OverlapPoint(positionTarget))
        {
                    // Reset, буде встановлено на наступному кадрі
                    positionTarget = Vector2.zero;
        }

                // Перейти до точки
                Vector3 direction = ((Vector3)positionTarget - this.transform.position);

                // Не забудьте скористатися сценарієм переміщення
             moveScript.direction = Vector3.Normalize(direction);
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
    // -- стрільба
    foreach (WeaponScript weapon in weapons)
    {
      weapon.enabled = true;
    }

        // Зупинити основну прокрутку
        foreach (ScrollingScript scrolling in FindObjectsOfType<ScrollingScript>())
    {
      if (scrolling.isLinkedToCamera)
      {
        scrolling.speed = Vector2.zero;
      }
    }
  }

  void OnTriggerEnter2D(Collider2D otherCollider2D)
  {
        //  Приймаючи урон? Зміна анімації
        ShotScript shot = otherCollider2D.gameObject.GetComponent<ShotScript>();
    if (shot != null)
    {
      if (shot.isEnemyShot == false)
      {
                // Зупинити атаки і почати віддалятися
                aiCooldown = Random.Range(minAttackCooldown, maxAttackCooldown);
        isAttacking = false;

                // Зміна анімації
                animator.SetTrigger("Hit");
      }
    }
  }

  void OnDrawGizmos()
  {
        // Невеликий відгук: ви можете відображати інформацію про налагодження у вашій сцені з Gizmos
        if (hasSpawn && isAttacking == false)
    {
      Gizmos.DrawSphere(positionTarget, 0.5f);
    }
  }
}
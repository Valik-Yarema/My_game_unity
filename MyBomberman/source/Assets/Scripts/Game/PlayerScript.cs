using UnityEngine;

/// <summary>
/// Контролер і поведінка гравця
/// </summary>
public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// 0 - швидкість bomberman
    /// </summary>
    public Vector2 speed = new Vector2(25, 25);

    // 1 - Зберігaє рух
    private Vector2 movement;
    private Rigidbody2D rigidBodyComponent;

    void Update()
    {  
        
        // 2 - Отримати інформацію про вісь
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // 3 - Movement per direction
        movement = new Vector2(
          speed.x * inputX,
          speed.y * inputY);

        // 5 - постріл
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2"); // Для користувачів Mac Ctrl + стрілка - не підходить

        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null && weapon.CanAttack)
            {
                weapon.Attack(false);
                SoundEffectsHelper.Instance.MakePlayerShotSound();
            }
        }

        // 6 - Переконайтеся, що ми не зв'язані з камерою
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
                  Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
                  Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
                  transform.position.z
                  );
    }

    void FixedUpdate()
    {
        //4 - Перемістити об'єкт гри
        if (rigidBodyComponent == null) rigidBodyComponent = GetComponent<Rigidbody2D>();
        rigidBodyComponent.velocity = movement;
    }

    void OnDestroy()
    {    
        // Перевірте, чи програвач мертвий, так як ми також називаємо при закритті Unity
        HealthScript playerHealth = this.GetComponent<HealthScript>();
        if (playerHealth != null && playerHealth.hp <= 0)
        {
            // Game Over.
            var gameOver = FindObjectOfType<GameOverScript>();
            gameOver.ShowButtons();
        }
     //==========
     //використати перевіркку в лічильнику
     //
     //==============
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

// зіткнення з ворогом
        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            // Вбий ворога
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp);

            damagePlayer = true;
        }

        // Collision with the boss
        BossScript boss = collision.gameObject.GetComponent<BossScript>();
        if (boss != null)
        {
            // Бос теж втрачає деякий к.с.
            HealthScript bossHealth = boss.GetComponent<HealthScript>();
            if (bossHealth != null) bossHealth.Damage(5);

            damagePlayer = true;
        }

        // Пошкодження гравця
        if (damagePlayer)
        {
            HealthScript playerHealth = this.GetComponent<HealthScript>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }
}

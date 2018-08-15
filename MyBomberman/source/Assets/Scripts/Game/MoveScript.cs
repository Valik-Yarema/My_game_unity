using UnityEngine;

/// <summary>
/// Просто переміщує поточний об'єкт гри
/// </summary>
public class MoveScript : MonoBehaviour
{
    // 0 - Дизайнерські змінні

    /// <summary>
    /// Скорочення проекції
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Переміщення напрямку
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);
	
  private Vector2 movement;

  void Update()
  {
        // 1 - Рух
        movement = new Vector2(
      speed.x * direction.x,
      speed.y * direction.y);
  }

  void FixedUpdate()
  {
        // Застосувати рух до  rigidbody
        GetComponent<Rigidbody2D>().velocity = movement;
  }
}
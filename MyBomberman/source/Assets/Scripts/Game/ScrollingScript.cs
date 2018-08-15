using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Сценарій прокручування паралакса, який слід призначити шару
/// </summary>
public class ScrollingScript : MonoBehaviour
{
    /// <summary>
    /// Швидкість прокрутки
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Переміщення напрямку
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    /// <summary>
    /// Рух слід застосувати до камери
    /// </summary>
    public bool isLinkedToCamera = false;

    /// <summary>
    /// Background  нескінченний
    /// </summary>
    public bool isLooping = false;

  private List<Transform> backgroundPart;
  private Vector2 repeatableSize;
  private Vector3 gap = Vector3.zero;

  void Start()
  {
        // Тільки для нескінченного фону
        if (isLooping)
    {
            //---------------------------------------------------------------------------------
            // 1 - Отримати фонові об'єкти
            // - Ми повинні знати, з чого складається цей фон
            // - Зберігати посилання на кожен об'єкт
            // - замовити ці елементи у порядку прокрутки, тому ми знаємо елемент, який буде першим, який буде перероблений
            // - Обчислити відносне положення між кожною частиною перед початком руху
            // ------------------------------------------------ ---------------------------------

            // Отримати всю частину шару
            backgroundPart = new List<Transform>();

      for (int i = 0; i < transform.childCount; i++)
      {
        Transform child = transform.GetChild(i);

// тільки видимі нащадки
        if (child.GetComponent<Renderer>() != null)
        {
          backgroundPart.Add(child);
          
          // перший елемент
          if (backgroundPart.Count == 1)
          {
                        // Gap - це пробіл між нулем і першим елементом.
                        // нам це потрібно, коли ми петляємо.
                        gap = child.transform.position;
            
            if (direction.x == 0) gap.x = 0;
            if (direction.y == 0) gap.y = 0;
          }
        }
      }

      if (backgroundPart.Count == 0)
      {
        Debug.LogError("Nothing to scroll!");
      }      
// Сортувати за позицією
// - залежить від напрямку прокрутки
            backgroundPart = backgroundPart.OrderBy(t => t.position.x * (-1 * direction.x)).ThenBy(t => t.position.y * (-1 * direction.y)).ToList();

            // Отримати розмір повторюваних частин
            var first = backgroundPart.First();
      var last = backgroundPart.Last();

      repeatableSize = new Vector2(
        Mathf.Abs(last.position.x - first.position.x),
        Mathf.Abs(last.position.y - first.position.y)
        );
    }
  }

  void Update()
  {
    // рух
    Vector3 movement = new Vector3(
      speed.x * direction.x,
      speed.y * direction.y,
      0);

    movement *= Time.deltaTime;
    transform.Translate(movement);

    // рух камери
    if (isLinkedToCamera)
    {
      Camera.main.transform.Translate(movement);
    }

        // цикл
        if (isLooping)
    {
            //---------------------------------------------------------------------------------
            // 2 - Перевірте, чи є об'єкт попереднім, в межах або за межами камери
            //---------------------------------------------------------------------------------

            // межі камери
            var dist = (transform.position - Camera.main.transform.position).z;
      float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
      float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
//      float width = Mathf.Abs(rightBorder - leftBorder);

      var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
      var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
            //      float height = Mathf.Abs(topBorder - bottomBorder);

            // Визначення входу та виходу з кордону за допомогою напрямку
            Vector3 exitBorder = Vector3.zero;
      Vector3 entryBorder = Vector3.zero;

      if (direction.x < 0)
      {
        exitBorder.x = leftBorder;
        entryBorder.x = rightBorder;
      }
      else if (direction.x > 0)
      {
        exitBorder.x = rightBorder;
        entryBorder.x = leftBorder;
      }

      if (direction.y < 0)
      {
        exitBorder.y = bottomBorder;
        entryBorder.y = topBorder;
      }
      else if (direction.y > 0)
      {
        exitBorder.y = topBorder;
        entryBorder.y = bottomBorder;
      }

            // Отримати перший об'єкт
            Transform firstChild = backgroundPart.FirstOrDefault();

      if (firstChild != null)
      {
        bool checkVisible = false;

                // Перевірте, чи є ми після камери
                // Перевірка знаходиться на першому місці, оскільки IsVisibleFrom є важким методом
                // Тут знову ми перевіряємо кордон залежно від напрямку
                if (direction.x != 0)
        {
          if ((direction.x < 0 && (firstChild.position.x < exitBorder.x))
          || (direction.x > 0 && (firstChild.position.x > exitBorder.x)))
          {
            checkVisible = true;
          }
        }
        if (direction.y != 0)
        {
          if ((direction.y < 0 && (firstChild.position.y < exitBorder.y))
          || (direction.y > 0 && (firstChild.position.y > exitBorder.y)))
          {
            checkVisible = true;
          }
        }

                // Перевірте, чи спрайт дійсно видно на камері чи ні
                if (checkVisible)
        {
                    //---------------------------------------------------------------------------------
                    // 3 - Об'єкт знаходився в межах камери, але більше немає.
                    // - Ми повинні його переробити
                    // - Це означає, що він перший, він тепер останній
                    // - І ми фізично переміщаємо його до подальшої позиції
                    //---------------------------------------------------------------------------------

                    if (firstChild.GetComponent<Renderer>().IsVisibleFrom(Camera.main) == false)
          {
                        // Встановити позицію в кінці
                        firstChild.position = gap + new Vector3(
              firstChild.position.x + ((repeatableSize.x + firstChild.GetComponent<Renderer>().bounds.size.x) * -1 * direction.x),
              firstChild.position.y + ((repeatableSize.y + firstChild.GetComponent<Renderer>().bounds.size.y) * -1 * direction.y),
              firstChild.position.z
              );

                        // Перша частина стає останньою
                        backgroundPart.Remove(firstChild);
            backgroundPart.Add(firstChild);
          }
        }
      }

    }
  }
}

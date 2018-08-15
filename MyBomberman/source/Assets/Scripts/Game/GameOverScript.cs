using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Почніть або закрийте гру
/// </summary>
public class GameOverScript : MonoBehaviour
{
    private Button[] buttons;

    void Awake()
    {
        // Отримати кнопки
        buttons = GetComponentsInChildren<Button>();

        // Вимкнути їх
        HideButtons();
    }

    public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    public void ExitToMenu()
    {
        // завантажити меню
        Application.LoadLevel("Menu");
    }

    public void RestartGame()
    {
        // Перезавантажити рівень
        Application.LoadLevel("Stage1");
    }
}
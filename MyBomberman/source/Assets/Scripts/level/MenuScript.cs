using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Сценарій титульного екрана
/// </summary>
public class MenuScript : MonoBehaviour
{
    public void StartGame()
    {
        // "Stage1" - це назва першої сцени, яку ми створили.
        SceneManager.LoadScene("Stage1");
        
    }
}

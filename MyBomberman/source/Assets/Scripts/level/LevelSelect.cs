using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {


    public void StartLevel1()
    {
        // "Stage01" - це назва першої сцени, яку ми створили.
      //  Application.LoadLevel("Stage01");
        SceneManager.LoadScene(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels_wiuv : MonoBehaviour
{
   // public int lev = 1; 

    public void StartLevel1(int lev)
    {
        // "scene1" - це назва першої сцени, яку ми створили.
               
        
        
            switch (lev)
        {
            case 0: { SceneManager.LoadScene(lev); break; }
            case 1: { SceneManager.LoadScene(lev); break; }
            case 2: { SceneManager.LoadScene(lev); break; }
            case 3: { SceneManager.LoadScene("Victory"); break; }
            default: { SceneManager.LoadScene(lev); break; }
        }
        
    }
    public void StartBeackLevel1(int lev)
    {
        // "scene1" - це назва першої сцени, яку ми створили.



        switch (lev)
        {

            case 0: { SceneManager.LoadScene("Menu"); break; }
            case 1: { SceneManager.LoadScene("Levels"); break; }
            case 3: { SceneManager.LoadScene("Victory"); break; }
            case 2: { SceneManager.LoadScene("Record"); break; }
            default: { SceneManager.LoadScene(lev); break; }

                //якщо зробити рівнв то дописати
        }

    }
}

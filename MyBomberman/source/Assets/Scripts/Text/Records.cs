using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Records : MonoBehaviour {   
    private string namePlayer;
  
    public Text txt1;
    public GameObject Panel;

    // public GameObject Record;
    // Use this for initialization
    void Start()
    {


    }


       public void allrecord()
        {


        txt1.text = File.ReadAllText(@"D:\NameWin.txt");


        }
    
}


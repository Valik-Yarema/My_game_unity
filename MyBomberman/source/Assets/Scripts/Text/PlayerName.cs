using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class PlayerName : MonoBehaviour {
    public InputField inputF;
    public string Name;
    public GameObject Panel;


    void Start() {

    }


    void Update() {
        Name = inputF.textComponent.text;
    }
    public void ApplyName()
    {
        string all1 = File.ReadAllText(@"D:\NameWin.txt", Encoding.Default);

        StreamWriter sw = new StreamWriter(@"D:\NameWin.txt");
        sw.WriteLine(all1+Name);

        sw.Close();

        Panel.SetActive(false);
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeBackgroundColor : MonoBehaviour {

    public Camera bg;
    Color[] colors = new Color[7];
    int i = 0;
	// Use this for initialization
	void Start () {
        colors[0] = Color.cyan;
        colors[1] = Color.red;
        colors[2] = Color.green;
        colors[3] = Color.blue;
        colors[4] = Color.yellow;
        colors[5] = Color.magenta;
        colors[6] = Color.white;
       

        // bg = FindObjectOfType<"main camera">
    }
	
	// Update is called once per frame
	void Update () {

        bg.backgroundColor = colors[i];
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(500, 500, 300, 100), "CHANGE"))
        {
            i = i + 1;
            if (i > colors.Length-1)
            {
                i = 0;
            }
        }
           
    }
}

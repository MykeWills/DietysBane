using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinText : MonoBehaviour {

    Text currentText;
    public bool UseSelection;

	// Use this for initialization
	void Start () {
        currentText = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        if(UseSelection)
        currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, Mathf.PingPong(Time.time, 1));
        else
            currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, currentText.color.a);
    }
    public void Selected()
    {
        UseSelection = true;
    }
    public void DeSelected()
    {
        UseSelection = false;
        currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, 1);
    }
}

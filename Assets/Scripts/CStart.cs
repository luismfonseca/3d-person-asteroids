using UnityEngine;
using System.Collections;

public class CStart : MonoBehaviour {

	public GUIStyle style = new GUIStyle();

	
	public void Start()
    {
    }
	
    void OnGUI() {
     
		int width = Screen.width;
		int height = Screen.height;
		Rect buttonRect = new Rect ((width/2-60), (height/2)+80, 120, 30);
		Rect labelRect = new Rect (0, (height/8), width, (height/4));
		style.fontSize = width/12;
		
		GUI.Label(labelRect, "ASTEROIDS", style);
		
		 if (GUI.Button(buttonRect, "START GAME")) {
	
	        Application.LoadLevel("level1");
	
	    }
	}
	
	
	public void Update() 
	{
		if (Input.GetKey(KeyCode.F2) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter)) {
			Application.LoadLevel("level1");
		}
	}
}

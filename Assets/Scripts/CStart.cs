using UnityEngine;
using System.Collections;

public class CStart : MonoBehaviour {

	public Texture btnTexture;
	public GUIStyle style = new GUIStyle();

	
	public void Start()
    {
        style.normal.textColor = new Color(256f, 256f, 256f);
        style.hover.textColor = new Color(0f, 256f, 256f);

 		style.alignment = TextAnchor.UpperCenter;
        style.font = (Font)Resources.Load("Vector Battle");
    }
	
    void OnGUI() {
        if (!btnTexture) {
            Debug.LogError("Please assign a texture on the inspector");
            return;
        }
		int width = Screen.width;
		int height = Screen.height;
		Rect buttonRect = new Rect ((width/2-60), (height/2)+80, 120, 30);
		Rect labelRect = new Rect (0, (height/4), width, (height/2));
		style.fontSize = width/16;
		
		GUI.Label(labelRect, "ASTEROIDS", style);
		
		 if (GUI.Button(buttonRect, "START GAME")) {
	
	        Application.LoadLevel("level1");
	
	    }
	}
}

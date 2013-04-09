using UnityEngine;
using System.Collections;

public class CStart : MonoBehaviour {

	 public Texture btnTexture;
    void OnGUI() {
        if (!btnTexture) {
            Debug.LogError("Please assign a texture on the inspector");
            return;
        }

		 if (GUI.Button (new Rect ((Screen.width/2-60), (Screen.height/2)+80, 120, 30), "START GAME")) {
	
	        Application.LoadLevel("level1");
	
	    }
	}
}

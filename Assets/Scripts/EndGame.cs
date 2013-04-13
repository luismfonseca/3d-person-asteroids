using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	public Texture btnTexture;
	void OnGUI() {
		if (!btnTexture) {
			Debug.LogError("Please assign a texture on the inspector");
			return;
		}
		
		GUIStyle guiStyle = new GUIStyle();
		guiStyle.font = Resources.Load("Vector Battle", typeof(Font)) as Font;
		guiStyle.fontSize = 30;
		guiStyle.fontStyle = FontStyle.Normal;
		GUI.Label(new Rect(20, 20, 200, 200), "sssss");
	}
	
}

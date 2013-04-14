using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour{
	public float timer = 4f;
	public string levelToLoad = "start";

	public GUIStyle style = new GUIStyle();
	
	void Start() {
		StartCoroutine("DisplayScene"); 
	}

	IEnumerator DisplayScene() {
		yield return new WaitForSeconds(timer);
		Application.LoadLevel(levelToLoad);
	}
	
	void OnGUI(){
		int width = Screen.width;
		int height = Screen.height;
		Rect labelRect = new Rect (width/16, (height/2), 0, (height/2));
		style.fontSize = width/32;

		
		string text = 
			@"Omar Alejandro Castillo de Castro
Luis Miguel Guimaraes Pimentel Fonseca
Witold Zgrabka

present:";
		
		GUI.Label(labelRect,text,style);
	}
	
	
}
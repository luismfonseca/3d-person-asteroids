using UnityEngine;
using System.Collections;

public class Prologue : MonoBehaviour {
	private float time = 0f;
	public int textScrollSpeed = 3;
	public AudioClip backgroundMusic; 
	
	public GUIStyle style = new GUIStyle();
	
	void Start() {
		StartCoroutine("DisplayScene");
		if (backgroundMusic != null) {
			SceneSoundManager.PlayClipAt(backgroundMusic, Vector3.zero);
		}
	}

	IEnumerator DisplayScene() {
		yield return new WaitForSeconds( 4 * 30 / textScrollSpeed);
		Application.LoadLevel("start");
	}
	
	void Update () {
		
		// skip prologue
		if (Input.GetKey(KeyCode.Space)
		 || Input.GetKey(KeyCode.Escape)
		 || Input.GetKey(KeyCode.KeypadEnter)
		 || Input.GetKey(KeyCode.F2)) {
		
			Application.LoadLevel("start");
		}
	}
	
	void OnGUI() {
		time += Time.deltaTime;
		Rect labelRect = new Rect(Screen.width/16, Screen.height - textScrollSpeed * 5f * time,0, (Screen.height/2));
		style.fontSize = Screen.width / 32;
		
		string text = 
			@"ASTEROIDS

A long time ago
in a galaxy far, far away...
It is a period of civil war.
Rebel spaceships, striking from
a hidden base (FEUP)
are fighting the evil
Galactic Empire,
and some random asteroids.


Planet EARTH is under attack,
You were sent to save it.";
		
		GUI.Label(labelRect, text, style);
	}
}

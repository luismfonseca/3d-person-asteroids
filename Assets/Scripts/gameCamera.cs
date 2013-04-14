using UnityEngine;
using System.Collections;

public class gameCamera : MonoBehaviour {
	MonoBehaviour scene;

	// Use this for initialization
	void Start () {
		scene = gameObject.GetComponent<MonoBehaviour>();
	}
	
	// Update is called once per frame
	void Update () {
		

		//apply rotation to the "root" scene (the scene that includes the spaceship)
		//without changing the tranform origin,
		scene.transform.localEulerAngles = new Vector3(0, 0, -playerspaceship.rotation);
		if(GameControls.isPause && !GameControls.pausePressed){
			TooglePause();
		}
	}
	
	public static void unPause(){
		GameControls.paused = false;
		Time.timeScale = 1f;
	}
	
	public static void pause(){
		GameControls.paused = false;
		Time.timeScale = 1f;
	}
	
	public static void TooglePause(){
		GameControls.paused = !GameControls.paused;
		Time.timeScale = (GameControls.paused) ? 0f : 1f;
	}
	
	void OnGUI() {
		if(!GameControls.paused) return;
		if (GUI.Button (new Rect ((Screen.width/2 - 60), (Screen.height/2) + 40, 120, 30), "CONTINUE")) {
			unPause();
		}
		if(GUI.Button (new Rect ((Screen.width/2 - 60), (Screen.height/2), 120, 30), "START GAME")) {
	        Application.LoadLevel("level1");
			unPause();
	    }
	}
}
	
	
	


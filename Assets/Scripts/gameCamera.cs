using UnityEngine;
using System.Collections;

public class gameCamera : MonoBehaviour {
	public Texture btnTexture;	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		//return;

		//apply rotation to the "root" scene (the scene that includes the spaceship)
		//without changing the tranform origin,
		MonoBehaviour scene = gameObject.GetComponent<MonoBehaviour>();
				scene.transform.localEulerAngles = new Vector3(0, 0, -playerspaceship.rotation);
		if(xa.isPause && !xa.pausePressed){
			xa.paused = !xa.paused;
			Time.timeScale = (xa.paused) ? 0f : 1f;
		}
	}
	
	void OnGUI() {
		if(!xa.paused) return;
		if (!btnTexture) {
            Debug.LogError("Please assign a texture on the inspector");
            return;
        }
		
		if (GUI.Button (new Rect ((Screen.width/2 - 60), (Screen.height/2) + 40, 120, 30), "CONTINUE")) {
			xa.paused = false;
			Time.timeScale = 1f;
		}
		if(GUI.Button (new Rect ((Screen.width/2 - 60), (Screen.height/2), 120, 30), "START GAME")) {
	
	        Application.LoadLevel("level1");
	    }
	}
}
	
	
	


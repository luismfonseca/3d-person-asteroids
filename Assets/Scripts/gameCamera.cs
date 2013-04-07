using UnityEngine;
using System.Collections;

public class gameCamera : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		//return;

		//apply rotation to the "root" scene (the scene that includes the spaceship)
		//without changing the tranform origin,
		MonoBehaviour scene = gameObject.GetComponent<MonoBehaviour>();
				scene.transform.localEulerAngles = new Vector3(0,0,-playerspaceship.rotation);

	
	}
}

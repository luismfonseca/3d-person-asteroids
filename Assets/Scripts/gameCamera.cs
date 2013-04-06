using UnityEngine;
using System.Collections;

public class gameCamera : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		//return;
		// apply rotation on everything
		MonoBehaviour[] components = gameObject.GetComponentsInChildren<MonoBehaviour>();
		
		foreach(var component in components) {
		}
	}
}

using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
		//the scene position should be the opposite of the spaceship
		//the rotation should be done on the "root" scene (in game Camera)
		//because the origin position changes
		this.transform.localPosition = -playerspaceship.originalPosition;
	}
}

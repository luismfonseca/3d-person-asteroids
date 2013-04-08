using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour{
	public float timer =4f;
	public string levelToLoad="start";

	
	//start

	void Start(){

		StartCoroutine ("DisplayScene"); 
	}

	IEnumerator DisplayScene(){
		 yield return new WaitForSeconds(timer);
		Application.LoadLevel(levelToLoad);

	}
	
	
}


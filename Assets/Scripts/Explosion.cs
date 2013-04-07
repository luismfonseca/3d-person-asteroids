using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour{
	private OTSprite sprite;
	private float lifeTime;

	
	void Awake() 
	{
	}
	
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		lifeTime = 1;
	}
	
	// Update is called once per frame
	void Update () {
		lifeTime -= Time.deltaTime;
		if(lifeTime < 0){
			OT.DestroyObject(sprite);
			lifeTime = 1;
			return;
		}
	}
}

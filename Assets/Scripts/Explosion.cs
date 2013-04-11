using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour{
	private OTSprite sprite;
	Vector2 maxSize = new Vector2(2,2);
	public static readonly float maxLifeTime= 1f;
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
		if(sprite == null){
			return;	
		}
		lifeTime += Time.deltaTime;
		if(lifeTime >= maxLifeTime){
			OT.DestroyObject(sprite);
			lifeTime = 0;
			return;
		}
		
		sprite.size = maxSize * (Mathf.Sin(Mathf.PI * (lifeTime/maxLifeTime)));
	}
}

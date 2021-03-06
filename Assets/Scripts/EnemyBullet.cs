using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour{
	private OTSprite sprite;
	private float speed = 12;
	private float lifeTime;
	public static float bulletLifeTime = 0.8f;
	private Vector2 movedirection;

	
	void Awake() 
	{
	}
	
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		lifeTime = bulletLifeTime;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.name.StartsWith("asteroid") || 
			other.name.StartsWith("spaceship")){ // other asteroids don't affect us
			OT.DestroyObject(sprite);

		}
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GameControls.paused){ return;}
		if(lifeTime >= bulletLifeTime){
			movedirection = sprite.yVector;
			sprite.rotation -= playerspaceship.rotation;
		}
		float deltaTime = Time.deltaTime;
		lifeTime -= deltaTime;
		if(lifeTime < 0){
			OT.DestroyObject(sprite);
			lifeTime = bulletLifeTime;
			return;
		}
		sprite.position += movedirection * speed * deltaTime;
		//thisTransform.TransformPoint(.1f * Mathf.Cos(angle), .1f * Mathf.Sin(angle), 0);
		
		
		// check borders
		if (sprite.position.x > 9f)
			sprite.position = new Vector2(-9f, sprite.position.y);
		else if (sprite.position.x < -9f)
			sprite.position = new Vector2(9f, sprite.position.y);
		else if (sprite.position.y > 9f)
			sprite.position = new Vector2(sprite.position.x, -9f);
		else if (sprite.position.y < -9f)
			sprite.position = new Vector2(sprite.position.x, 9f);
		
	}
	
	
}


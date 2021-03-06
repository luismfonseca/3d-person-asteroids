using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour{
	private OTSprite sprite;
	private float speed = 13f;
	private float lifeTime;
	public static float bulletLifeTime = 1f;
	private Vector2 movedirection;
	
	void Awake() 
	{
	}
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		lifeTime = bulletLifeTime;
		playerspaceship.sprite.rotation = playerspaceship.rotation;
		movedirection = playerspaceship.sprite.yVector;
		playerspaceship.sprite.rotation = 0;
		sprite.position += movedirection/2;
	}
	
	public void initialize(){
		lifeTime = bulletLifeTime;
		if(sprite == null) return;
		sprite.rotation = playerspaceship.rotation;
		movedirection = sprite.yVector;
		sprite.rotation = 0;
		sprite.position += movedirection/2;

	}
	
	// Update is called once per frame
	void Update () {
		if (GameControls.paused) {
			return;
		}
		
		float deltaTime = Time.deltaTime;
		lifeTime -= deltaTime;
		if(lifeTime < 0){
			OT.DestroyObject(sprite);
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
	
		void OnTriggerEnter(Collider other){
			OTSprite collisionSprite = other.GetComponent<OTSprite>();

		if (other.name.StartsWith("enemy")){
			Scene.destroyEnemyShip(collisionSprite);
			Scene.AddPoints(100);
			OT.DestroyObject(sprite);

		}
		else if	(other.name.StartsWith("asteroid")){ // other asteroids don't affect us
			var asteroidSize = collisionSprite.size.x;
			if (asteroidSize >= 1.4f) {
				Scene.AddPoints(10);
			}
			else if (asteroidSize >= 0.8f) {
				Scene.AddPoints(20);
			}
			else if (asteroidSize >= 0.6f) {
				Scene.AddPoints(50);
			}
			else {
				Scene.AddPoints(100);
			}
			OT.DestroyObject(sprite);
		}	

	}
	
}

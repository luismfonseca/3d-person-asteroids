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

		sprite.onCollision = OnCollision; 
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
		if(xa.paused){ return;}
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
	
	public void OnCollision(OTObject owner)
    {
		OTSprite collisionSprite = owner.collisionObject as OTSprite;
		if(collisionSprite.protoType == "enemy"){
			OTSprite explosion = OT.CreateSprite("explosion");
			explosion.transform.parent = collisionSprite.transform.parent;
			explosion.transform.localPosition = collisionSprite.transform.localPosition;
			OT.DestroyObject(collisionSprite);
			OT.DestroyObject(owner);	
		}
		else if (collisionSprite.protoType.StartsWith("asteroid")) {
			OT.DestroyObject(this.GetComponent<MonoBehaviour>());
		}
	}
	
	
}



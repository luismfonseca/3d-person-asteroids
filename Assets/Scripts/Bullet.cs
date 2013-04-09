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
		sprite.onCollision = OnCollision; 
	}
	
	// Update is called once per frame
	void Update () {
		if(xa.paused){ return;}
		if(lifeTime >= bulletLifeTime){
			movedirection = sprite.yVector;
			sprite.rotation = 0;
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
	
	public void OnCollision(OTObject owner)
    {
		OTSprite colisionSprite = owner.collisionObject as OTSprite;
		if(colisionSprite.protoType == "enemy"){
			OTSprite explosion = OT.CreateSprite("explosion");
			explosion.transform.parent = colisionSprite.transform.parent;
			explosion.transform.localPosition = colisionSprite.transform.localPosition;
			OT.DestroyObject(colisionSprite);
			OT.DestroyObject(owner);	
		} //else if(sprite.protoType.StartsWith("asteroid")){
		//	OT.DestroyObject(owner);	
		//}
		
	}
	
	
}



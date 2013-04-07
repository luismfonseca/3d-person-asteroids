using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour{
	private OTSprite sprite;
	private float speed = 10;
	private Vector3 rotationSpeed;
	private float lifeTime;
	
	void Awake() 
	{
	}
	
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		lifeTime = 4;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = Time.deltaTime;
		lifeTime -= deltaTime;
		if(lifeTime < 0){
			OT.DestroyObject(sprite);
			lifeTime = 4;
			return;
		}
		sprite.position += sprite.yVector * speed * deltaTime;
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


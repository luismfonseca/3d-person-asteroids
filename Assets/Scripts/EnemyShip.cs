using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour{
	private Vector2 position;
	private float angle;
	private OTSprite sprite;
	private Vector2 speed;
	private Vector3 rotationSpeed;
	private float reloadTime = 2f;
	
	void Awake() 
	{
	}
	
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		angle = Random.value * 360f;
		position = sprite.position;
		speed = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))* (Random.value+0.5f) * 2;
		sprite.rotation = Random.value*360f;
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = Time.deltaTime;
		reloadTime -= deltaTime;
		if(reloadTime < 0){
			reloadTime += 2f;
			OTSprite bullet = OT.CreateSprite("enemyBullet");
			bullet.transform.parent = transform.parent;
			bullet.transform.localPosition = transform.localPosition;
			bullet.RotateTowards(playerspaceship.originalPosition);

			
		}
		
		position += speed * deltaTime;
		this.transform.localPosition = position;
		//thisTransform.TransformPoint(.1f * Mathf.Cos(angle), .1f * Mathf.Sin(angle), 0);
		
		
		// check borders
		if (position.x > 9f)
			position = new Vector2(-9f, position.y);
		else if (position.x < -9f)
			position = new Vector2(9f, position.y);
		else if (position.y > 9f)
			position = new Vector2(position.x, -9f);
		else if (position.y < -9f)
			position = new Vector2(position.x, 9f);
		
	}
}


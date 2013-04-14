using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour{
	private float angle;
	private OTSprite sprite;
	private Vector2 speed;
	private Vector3 rotationSpeed;
	private float reloadTime = 2f;
	
	private static readonly float BORDER_LIMITS = Scene.BORDER_LIMITS;
	
	void Awake() 
	{
	}
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		angle = Random.value * 360f;
		speed = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))* (Random.value+0.5f) * 2;
		sprite.rotation = Random.value*360f;
	}
	
	// Update is called once per frame
	void Update () {
		if(GameControls.paused){ return;}
		float deltaTime = Time.deltaTime;
		reloadTime -= deltaTime;
		if(reloadTime < 0){
			reloadTime += 2f;
			OTSprite bullet = OT.CreateSprite("enemyBullet");
			bullet.transform.parent = transform.parent;
			bullet.transform.localPosition = transform.localPosition;
			bullet.RotateTowards(playerspaceship.originalPosition);
		}
		
		Vector2 newPosition = sprite.transform.localPosition;
		newPosition += speed * deltaTime;	
		
		// check borders
		if (newPosition.x > BORDER_LIMITS)
			newPosition.Set(-BORDER_LIMITS, newPosition.y);
		else if (newPosition.x < -BORDER_LIMITS)
			newPosition.Set(BORDER_LIMITS, newPosition.y);
		else if (newPosition.y > BORDER_LIMITS)
			newPosition.Set(newPosition.x, -BORDER_LIMITS);
		else if (newPosition.y < -BORDER_LIMITS)
			newPosition.Set(newPosition.x, BORDER_LIMITS);
		
		sprite.transform.localPosition = newPosition;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.name.StartsWith("asteroid") || other.name.StartsWith("spaceship")) {
			Scene.destroyEnemyShip(this.GetComponent<OTSprite>());
		}
	}
}


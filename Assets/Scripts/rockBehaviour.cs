using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	
	private Vector2 position;
	private float direction;
	private float angle;
	private OTSprite sprite;
	private Transform thisTransform;
	private Vector2 speed;
	private Vector3 rotationSpeed;
	
	void Awake() 
	{
		thisTransform = transform;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.name.StartsWith("asteroid")) // other asteroids don't affect us
			return;
		
		if (other.name.StartsWith("spaceship")) {
			if (playerspaceship.isDead())
				return;
		}
		
		Scene.SplitAsteroids(this.GetComponent<OTSprite>());
	}
	
	// Use this for initialization
	void Start () {
		direction = Random.value < 0.5 ? -1 : 1;
		sprite = GetComponent<OTSprite>();
		angle = Random.value * 360f;
		position = sprite.position;
		speed = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))* (Random.value+0.5f) * 2;
		rotationSpeed = new Vector3(0, 0, Random.value*60);
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = Time.deltaTime;
		thisTransform.Rotate(rotationSpeed * deltaTime,direction);
		
		position += speed * deltaTime;
		sprite.transform.localPosition = position;
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

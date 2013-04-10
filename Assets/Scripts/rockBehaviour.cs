using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	
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
		speed = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))* (Random.value+0.5f) * 2;
		rotationSpeed = new Vector3(0, 0, Random.value*60);
	}
	
	// Update is called once per frame
	void Update () {
		float deltaTime = Time.deltaTime;
		thisTransform.Rotate(rotationSpeed * deltaTime,direction);
		
		Vector2 newPosition = new Vector2(
			sprite.transform.localPosition.x + speed.x * deltaTime,
			sprite.transform.localPosition.y + speed.y * deltaTime);
		//thisTransform.TransformPoint(.1f * Mathf.Cos(angle), .1f * Mathf.Sin(angle), 0);
		
		
		// check borders
		if (newPosition.x > 9f)
			newPosition.Set(-9f, newPosition.y);
		else if (newPosition.x < -9f)
			newPosition.Set(9f, newPosition.y);
		else if (newPosition.y > 9f)
			newPosition.Set(newPosition.x, -9f);
		else if (newPosition.y < -9f)
			newPosition.Set(newPosition.x, 9f);
		
		sprite.transform.localPosition = newPosition;

		
	}
}

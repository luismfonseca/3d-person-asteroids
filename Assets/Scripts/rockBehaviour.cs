using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	
	private float direction;
	private float angle;
	private OTSprite sprite;
	private Transform thisTransform;
	private Vector2 speed;
	private Vector3 rotationSpeed;
	
	private static readonly float BORDER_LIMITS = Scene.BORDER_LIMITS;
	
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
		Vector2 newPosition = sprite.transform.localPosition;
		newPosition += speed * deltaTime * Scene.asteroidSpeedFactor;	
		
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
}

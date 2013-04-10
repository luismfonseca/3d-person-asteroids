using UnityEngine;
using System.Collections;

public class playerspaceship : MonoBehaviour {
	
	public static Vector2 speed;
	public static OTSprite sprite;
	
	public static float rotation;
	public static float deltaRotation;
	public static Vector2 position;
	public static Vector2 originalPosition;
	
	public static float deadSince;
	
	public static bool isDead() {
		return (deadSince != 0f);
	}
	
	void Awake() 
	{
	}
	
	// Use this for initialization
	void Start() {
		sprite = GetComponent<OTSprite>();
		originalPosition = Vector2.zero;
		speed = Vector2.zero;
		rotation = 0f;
		deadSince = 0f;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (isDead())
			return;
		
		if (other.name.StartsWith("bullet")) // our bullets dont do nothing
			return;
		
		else if (other.name.StartsWith("enemyBullet"))
		{
			Destroy(other.GetComponent<MonoBehaviour>());
		}
		else if (other.name.StartsWith("enemyShip"))
		{
			Destroy(other.GetComponent<MonoBehaviour>());
		}
		
		if (other.name.StartsWith("asteroid"))
		{
		}
		
		sprite.visible = false;
		deadSince = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update() {
		if (xa.paused)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		
		if (!isDead()) // still alive?
		{
			// rotation
			deltaRotation = xa.isLeft ? 160f *deltaTime: xa.isRight ? -160f *deltaTime: 0;
			rotation += deltaRotation;
			if(rotation > 360) {
				rotation -= 360;
			}
			else if(rotation < 0) {
				rotation += 360;
			}
	
			// movement
			if (xa.isUp) { // increase spaceship speed
				
				speed.x += 9f * Mathf.Cos((Mathf.PI / 180) * (rotation + 90)) * deltaTime;
				speed.y += 9f * Mathf.Sin((Mathf.PI / 180) * (rotation + 90)) * deltaTime;
			}
		}
		else if (Time.timeSinceLevelLoad - deadSince > 2)
		{
			Application.LoadLevel(Application.loadedLevel);
		}
		
		speed.x *= .98f;
		speed.y *= .98f;

		if (originalPosition.x > 9f)
			originalPosition = new Vector2(-9f, originalPosition.y);
		else if (originalPosition.x < -9f)
			originalPosition = new Vector2(9f, originalPosition.y);
		else if (originalPosition.y > 9f)
			originalPosition = new Vector2(originalPosition.x, -9f);
		else if (originalPosition.y < -9f)
			originalPosition = new Vector2(originalPosition.x, 9f);
		originalPosition += speed * deltaTime;
		//this.transform.localPosition = originalPosition;
		this.transform.localEulerAngles = new Vector3(0,0,rotation);
	}
}
using UnityEngine;
using System.Collections;

public class playerspaceship : MonoBehaviour {
	
	public static Vector2 speed;
	public static float rotationalSpeed;
	public static OTSprite sprite;
	
	public static Texture textureNormal = null;
	public static Texture texturePower = null;
	
	public static float rotation;
	public static float deltaRotation;
	public static Vector2 position;
	public static Vector2 originalPosition;
	
	public static float deadSince;
	
	private static readonly float BORDER_LIMITS = Scene.BORDER_LIMITS;
	
	public static bool isDead() {
		return (deadSince != 0f);
	}
	
	void Awake() 
	{
	}
	
	// Use this for initialization
	void Start() {
		if(textureNormal == null){
			textureNormal = Resources.Load("spaceship") as Texture;
			texturePower = Resources.Load("spaceship_power") as Texture;
		}
		sprite = GetComponent<OTSprite>();
		originalPosition = Vector2.zero;
		speed = Vector2.zero;
		rotation = 0f;
		deadSince = 0f;
		rotationalSpeed = 0f;
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
		
		sprite.visible = false;
		deadSince = Time.timeSinceLevelLoad;
		
		if (other.name.StartsWith("asteroid"))
		{
			Scene.SplitAsteroids(other.gameObject.GetComponent<OTSprite>());
		}
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
			deltaRotation = xa.isLeft ? 40f *deltaTime: xa.isRight ? -40f *deltaTime: 0;
			rotationalSpeed += deltaRotation;
			// movement
			if (xa.isUp && !Scene.Winner) { // increase spaceship speed
				sprite.image = texturePower;
				speed.x += 11f * Mathf.Cos((Mathf.PI / 180) * (rotation + 90)) * deltaTime;
				speed.y += 11f * Mathf.Sin((Mathf.PI / 180) * (rotation + 90)) * deltaTime;
			}
			else {
				sprite.image = textureNormal;
			}
		}
		else if (Time.timeSinceLevelLoad - deadSince > 2)
		{
			if (Scene.lifes != 0) {
				Application.LoadLevel(Application.loadedLevel);
			}
			else {
				Scene.GameIsOver = true;
			}
		}
		
		rotationalSpeed *= .85f;
		
		speed.x *= .98f;
		speed.y *= .98f;

		if (originalPosition.x > BORDER_LIMITS)
			originalPosition = new Vector2(-BORDER_LIMITS, originalPosition.y);
		else if (originalPosition.x < -BORDER_LIMITS)
			originalPosition = new Vector2(BORDER_LIMITS, originalPosition.y);
		else if (originalPosition.y > BORDER_LIMITS)
			originalPosition = new Vector2(originalPosition.x, -BORDER_LIMITS);
		else if (originalPosition.y < -BORDER_LIMITS)
			originalPosition = new Vector2(originalPosition.x, BORDER_LIMITS);
		originalPosition += speed * deltaTime;
		rotation += rotationalSpeed;
			if(rotation > 360) {
				rotation -= 360;
			}
			else if(rotation < 0) {
				rotation += 360;
			}
		
		//this.transform.localPosition = originalPosition;
		this.transform.localEulerAngles = new Vector3(0,0,rotation);
	}
}
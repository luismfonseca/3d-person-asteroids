using UnityEngine;
using System.Collections;

public class playerspaceship : MonoBehaviour {
	
	public static Vector2 speed;
	public OTSprite sprite;
	
	public static float rotation;
	public static float deltaRotation;
	public static Vector2 position;
	public static Vector2 originalPosition;
	
	void Awake() 
	{
	}
	
	// Use this for initialization
	void Start () {
		sprite = GetComponent<OTSprite>();
		originalPosition = Vector2.zero;
		speed = Vector2.zero;
		rotation = 0f;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(xa.paused){ return;}
		float deltaTime = Time.deltaTime;
		// rotation
		deltaRotation = xa.isLeft ? 100f *deltaTime: xa.isRight ? -100f *deltaTime: 0;
		rotation += deltaRotation;
		if(rotation > 360) rotation-=360;
		else if(rotation < 0) rotation+=360;

		// movement
		if (xa.isUp) { // increase spaceship speed
			speed.x += 5f * Mathf.Cos((Mathf.PI / 180) * (rotation + 90)) * deltaTime;
			speed.y += 5f * Mathf.Sin((Mathf.PI / 180) * (rotation + 90)) * deltaTime;
		}
		
		speed.x *= .99f;
		speed.y *= .99f;

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
using UnityEngine;
using System.Collections;

public class playerspaceship : MonoBehaviour {
	
	private Vector2 speed;
	private OTSprite sprite;
	public static float rotation;
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
		return;
		// rotation
		rotation += xa.isLeft ? 2f : xa.isRight ? -2f : 0;
		
		// movement
		if (xa.isUp) { // increase spaceship speed
			speed.x += .005f * Mathf.Cos((Mathf.PI / 180) * (rotation + 90));
			speed.y += .005f * Mathf.Sin((Mathf.PI / 180) * (rotation + 90));
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
		
		originalPosition += speed;
		this.transform.position = originalPosition;
		sprite.rotation = rotation;
	}
}
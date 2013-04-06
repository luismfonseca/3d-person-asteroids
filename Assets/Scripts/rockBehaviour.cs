using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	
	private Vector2 position;
	private float direction;
	private float angle;
	private OTSprite sprite;
	
	void Awake() 
	{Y
	}
	
	
	// Use this for initialization
	void Start () {
		direction = Random.value < 0.5 ? -1 : 1;
		sprite = GetComponent<OTSprite>();
		angle = Random.value * 360f;
		position = sprite.position;
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.Rotate(new Vector3(0, 0, 1), direction * 1f);
		
		position += new Vector2(.1f * Mathf.Cos(angle),
								.1f * Mathf.Sin(angle));
		sprite.position = position;
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

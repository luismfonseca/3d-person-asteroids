using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	
	private Vector2 position;
	private float direction;
	private float angle;
	private OTSprite sprite;
	private Transform thisTransform;
	private Vector2 move;
	
	void Awake() 
	{
		thisTransform = transform;

	}
	
	
	// Use this for initialization
	void Start () {
		direction = Random.value < 0.5 ? -1 : 1;
		sprite = GetComponent<OTSprite>();
		angle = Random.value * 360f;
		position = sprite.position;
		move = new Vector2(.1f * Mathf.Cos(angle), .1f * Mathf.Sin(angle));
	}
	
	// Update is called once per frame
	void Update () {
		thisTransform.Rotate(new Vector3(0, 0, 1), direction * 1f);
		
		position += move;
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

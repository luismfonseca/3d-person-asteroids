using UnityEngine;
using System.Collections;

public class xa : MonoBehaviour {

	public static bool isLeft;
	public static bool isRight;
	public static bool isUp;
	public static bool isDown;
	public static bool isShoot;
	public static bool isPause;

	public static bool shooting;
	public static bool paused;
	public static bool pausePressed;

	public void Start()
	{
	}

	public void Update() 
	{
		shooting = isShoot;
		pausePressed = isPause;
		// these are false unless one of keys is pressed
		isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
		isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
		isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
		isShoot = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter);
		isPause = Input.GetKey(KeyCode.P) || Input.GetKey(KeyCode.Escape);
	}
}

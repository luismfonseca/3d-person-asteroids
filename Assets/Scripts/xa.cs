using UnityEngine;
using System.Collections;

// static class courtesy of Michael Todd http://twitter.com/thegamedesigner
// This script is part of the tutorial series "Making a 2D game with Unity3D using only free tools"
// http://www.rocket5studios.com/tutorials/make-a-2d-game-in-unity3d-using-only-free-tools-part-1

public class xa : MonoBehaviour {

	//public static Scoring sc; // scoring will be added in an upcomming tutorial

	public static float orthSize;
	public static float orthSizeX;
	public static float orthSizeY;
	public static float camRatio;

	public static bool blockedRight = false;
	public static bool blockedLeft = false;
	public static bool blockedUp = false;
	public static bool blockedDown = false;

	public static float playerHitboxX = 0.225f; // player x = 0.45
	public static float playerHitboxY = 0.5f; // 0.5 is correct for ladders while player actual y = 0.6

	public static bool isLeft;
	public static bool isRight;
	public static bool isUp;
	public static bool isDown;
	public static bool isShoot;

	public static bool alive;
	public static bool onLadder;
	public static bool onRope;
	public static bool falling;
	public static bool shooting;

	public static int facingDir = 1; // 1 = left, 2 = right, 3 = up, 4 = down
	public enum anim { None, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }

	public static Vector3 glx;

	public void Start()
	{
		//sc = (Scoring)(this.gameObject.GetComponent("Scoring")); // scoring will be added in an upcomming tutorial

		// gather information from the camera to find the screen size
		xa.camRatio = 1.333f; // 4:3 is 1.333f (800x600) 
		xa.orthSize = Camera.mainCamera.camera.orthographicSize;
		xa.orthSizeX = xa.orthSize * xa.camRatio;
	}

	public void Update() 
	{
		// these are false unless one of keys is pressed
		isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
		isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
		isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
		isShoot = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E);
	}
}

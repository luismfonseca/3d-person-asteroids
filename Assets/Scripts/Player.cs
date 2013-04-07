using UnityEngine;
using System.Collections;

// This script is part of the tutorial series "Making a 2D game with Unity3D using only free tools"
// http://www.rocket5studios.com/tutorials/make-a-2d-game-in-unity3d-using-only-free-tools-part-1

public class Player : MonoBehaviour {

	// shoot objects
	private Transform shootParent;
	private Renderer shootRenderer;
	private OTAnimatingSprite shootSprite;

	// movement
	private float moveSpeed = 5;
	private int moveDirX;
	private int moveDirY;
	private Vector3 movement;
	private Transform thisTransform;
			
	// raycasts
	private float rayBlockedDistX = 0.6f;
	private RaycastHit hit;
	
	// layer masks	
	private int groundMask = 1 << 8; // layer = Ground
	private int shootMask = 1 << 8 | 1 << 9; // layers = Ground, Ladder
		
	private bool dropFromRope = false;
	private bool shotBlockedLeft;
	private bool shotBlockedRight;
	
	private Vector3 spawnPoint;
	private Vector3 ladderHitbox;
	
	void Awake() 
	{
		thisTransform = transform;
	}
	
	void Start()
    {
		xa.alive = true;
		spawnPoint = thisTransform.position; // player will respawn at initial starting point
		
		// connect external objects
		shootParent = transform.Find("shoot parent");
		shootRenderer = GameObject.Find("shoot").renderer;
		shootSprite = GameObject.Find("shoot").GetComponent<OTAnimatingSprite>();
    }
	
	/* ============================== CONTROLS ============================== */
	
	public void Update ()
	{		
		UpdateRaycasts();
		
		moveDirX = 0;
		moveDirY = 0;
		print(xa.isLeft);
		// move left
		if(xa.isLeft && !xa.blockedLeft && !xa.shooting) 
		{
			moveDirX = -1;
			xa.facingDir = 1;
		}
		
		// move right
		if(xa.isRight && !xa.blockedRight && !xa.shooting) 
		{
			moveDirX = 1;
			xa.facingDir = 2;
		}
		
		// move up on ladder
		if(xa.isUp && !xa.blockedUp && xa.onLadder)
		{
			moveDirY = 1;
			xa.facingDir = 3;
		}
		
		// move down on ladder
		if(xa.isDown && !xa.blockedDown && xa.onLadder) 
		{
			moveDirY = -1;
			xa.facingDir = 4;
		}
		
		// drop from rope
		if(xa.isDown && xa.onRope) 
		{
			xa.onRope = false;
			dropFromRope = true;
		}
		
		// shoot
		if (xa.isShoot && !xa.shooting && !xa.onRope && !xa.falling && !shotBlockedLeft && !shotBlockedRight) 
		{
			StartCoroutine(Shoot());
		}

		UpdateMovement();
	}
	
	void UpdateMovement() 
	{
		// player is not falling so move normally
		if(!xa.falling || xa.onLadder) 
		{
			movement = new Vector3(moveDirX, moveDirY,0f);
			movement *= Time.deltaTime*moveSpeed;
			thisTransform.Translate(movement.x,movement.y, 0f);
		}
		
		// player is falling so apply gravity
		else 
		{
			movement = new Vector3(0f,-1f,0f);
			movement *= Time.deltaTime*moveSpeed;
			thisTransform.Translate(0f,movement.y, 0f);
		}
	}
	
	/* ============================== RAYCASTS ============================== */
	
	void UpdateRaycasts() 
	{
		// set these to false unless a condition below is met
		xa.blockedRight = false;
		xa.blockedLeft = false;
		shotBlockedLeft = false;
		shotBlockedRight = false;
		
		// is the player is standing on the ground?
		// cast 2 rays, one on each side of the character
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.3f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.7f, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x+0.3f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.7f, groundMask))
		{	
			xa.falling = false;
			
			// snap the player to the top of a ground tile if she's not on a ladder
			if(!xa.onLadder)
			{
				thisTransform.position = new Vector3(thisTransform.position.x, hit.point.y + xa.playerHitboxY, 0f);
			}
		}
		
		// then maybe she's falling
		else
		{
			if(!xa.onRope && !xa.falling && !xa.onLadder) {
				xa.falling = true;
			}
		}
		
		// player is blocked by something on the right
		// cast out 2 rays, one from the head and one from the feet
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y+0.3f,thisTransform.position.z+1f), Vector3.right, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y-0.4f,thisTransform.position.z+1f), Vector3.right, rayBlockedDistX, groundMask))
		{
			xa.blockedRight = true;
		}
		
		// player is blocked by something on the left
		// cast out 2 rays, one from the head and one from the feet
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y+0.3f,thisTransform.position.z+1f), -Vector3.right, rayBlockedDistX, groundMask) || Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y-0.4f,thisTransform.position.z+1f), -Vector3.right, rayBlockedDistX, groundMask))
		{
			xa.blockedLeft = true;
		}
		
		// is there something blocking our shot to the right?
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z+1f), Vector3.right, 1f, shootMask))
		{
			shotBlockedRight = true;
		}
		
		// is there something blocking our shot to the left?
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.right, 1f, shootMask))
		{
			shotBlockedLeft = true;
		}
		
		// did the shot hit a brick tile to the left?
		if (Physics.Raycast(new Vector3(thisTransform.position.x-1f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.6f, groundMask))
		{
			if(!shotBlockedLeft && xa.isShoot && xa.facingDir == 1) {
				// breaking bricks will be added in an upcomming tutorial
				/*if (hit.transform.GetComponent<Brick>())
				{
					StartCoroutine(hit.transform.GetComponent<Brick>().PlayBreakAnim());
				}*/
			}
		}
		
		// did the shot hit a brick tile to the right?
		if(Physics.Raycast(new Vector3(thisTransform.position.x+1f,thisTransform.position.y,thisTransform.position.z+1f), -Vector3.up, out hit, 0.6f, groundMask))
		{
			if(!shotBlockedRight && xa.isShoot && xa.facingDir == 2) {
				// breaking bricks will be added in an upcomming tutorial
				/*if (hit.transform.GetComponent<Brick>())
				{
					StartCoroutine(hit.transform.GetComponent<Brick>().PlayBreakAnim());
				}*/
			}
		}
		
		// is the player on the far right edge of the screen?
		if (thisTransform.position.x + xa.playerHitboxX > (Camera.mainCamera.transform.position.x + xa.orthSizeX)) 
		{
			xa.blockedRight = true;
		}
		
		// is the player on the far left edge of the screen?
		if (thisTransform.position.x - xa.playerHitboxX < (Camera.mainCamera.transform.position.x - xa.orthSizeX)) 
		{
			xa.blockedLeft = true;
		}
	}	
	
	/* ============================== SHOOT ====================================================================== */
	
	IEnumerator Shoot()
	{
		xa.shooting = true;
		
		// show the shoot sprite and play the animation
		shootRenderer.enabled = true;
		shootSprite.Play("shoot");
		
		// check facing direction and flip the shoot parent to the correct side
		if(xa.facingDir == 1)
		{
			shootParent.localScale = new Vector3(1,1,1); // left side
		}
		if(xa.facingDir == 2)
		{
			shootParent.localScale = new Vector3(-1,1,1); // right side
		}
		
		yield return new WaitForSeconds(0.4f);
		
		// hide the sprite
		shootRenderer.enabled = false;
		xa.shooting = false;
	}
	
	/* ============================== DEATH AND RESPAWN ====================================================================== */
	
	void RespawnPlayer()
	{
		// respawn the player at her initial start point
		thisTransform.position = spawnPoint;
		xa.alive = true;
	}
	
	/* ============================== TRIGGER EVENTS ====================================================================== */
	
	void OnTriggerEnter(Collider other)
	{
		// did the player collide with a pickup?
		// pickups and scoring will be added in an upcomming tutorial
		/*if (other.gameObject.CompareTag("Pickup"))
		{
			if (other.GetComponent<Pickup>())
			{
				other.GetComponent<Pickup>().PickMeUp();
				xa.sc.Pickup();
			}
		}*/
	}
	
	void OnTriggerStay(Collider other)
	{
		// has the player been crushed by a block?
		// this will be added in an upcomming tutorial
		/*if (other.gameObject.CompareTag("Crusher"))
		{
			if(xa.alive)
			{
				xa.alive = false;
				RespawnPlayer();
				xa.sc.LifeSubtract();
			}
		}*/
		
		// is the player overlapping a ladder?
		if(other.gameObject.CompareTag("Ladder"))
		{
			xa.onLadder = false;
			xa.blockedUp = false;
			xa.blockedDown = false;
			
			ladderHitbox.y = other.transform.localScale.y * 0.5f; // get half the ladders Y height
			
			// is the player overlapping the ladder?
			// if player is landing on top of ladder from a fall, let him pass by
			if ((thisTransform.position.y + xa.playerHitboxY) < ((ladderHitbox.y + 0.1f) + other.transform.position.y))
			{
				xa.onLadder = true;
				xa.falling = false;
			}
			
			// if the player is at the top of the ladder, then snap her to the top
			if ((thisTransform.position.y + xa.playerHitboxY) >= (ladderHitbox.y + other.transform.position.y) && xa.isUp)
			{
				xa.blockedUp = true;
				xa.glx = thisTransform.position;
                xa.glx.y = (ladderHitbox.y + other.transform.position.y) - xa.playerHitboxY;
                thisTransform.position = xa.glx;
			}
			
			// if the player is at the bottom of the ladder, then snap her to the bottom
			if ((thisTransform.position.y - xa.playerHitboxY) <= (-ladderHitbox.y + other.transform.position.y))
			{
				xa.blockedDown = true;
				xa.glx = thisTransform.position;
				xa.glx.y = (-ladderHitbox.y + other.transform.position.y) + xa.playerHitboxY;
                thisTransform.position = xa.glx;
			}
		}
		
		// is the player overlapping a rope?
		if(other.gameObject.CompareTag("Rope"))
		{
			xa.onRope = false;
			
			if(!xa.onRope && !dropFromRope) 
			{
				// snap player to center of the rope
				if (thisTransform.position.y < (other.transform.position.y + 0.2f) && thisTransform.position.y > (other.transform.position.y - 0.2f))
                {
					xa.onRope = true;
					xa.falling = false;
					xa.glx = thisTransform.position;
                    xa.glx.y = other.transform.position.y;
                    thisTransform.position = xa.glx;
                }
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		// did the player exit a rope trigger?
		if (other.gameObject.CompareTag("Rope"))
		{
			xa.onRope = false;
			dropFromRope = false;
		}
		
		// did the player exit a ladder trigger?
		if (other.gameObject.CompareTag("Ladder")) 
		{
			xa.onLadder = false;
		}
	}
}

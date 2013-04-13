using UnityEngine;
using System.Collections;

/// <summary>
/// Scene Class, it contains all the objects expect the spaceship.
/// </summary>
public class Scene : MonoBehaviour {
	public static int lifes = 3;             //current number of lifes
	public static readonly int maxLifes = 3; //maximum number of lifes

	public static int points;              //game score
	public static bool GameIsOver = false; //true means the player lost the game
	public static bool Winner = false;     //true means the player won the game
	
	// timestamp to show when the user won the game, 
	// usefull to lock the user input when he won the game
	// avoiding the player to restart the game without noticing
	// that he won the game
	public float WinTimeStamp = 0;             
	public static float WinInputLockTime = 3f;

	public static int numberOfAsteroids = 0; //number on asteroids in the scene
	public static int numberOfEnemyShips = 0;//number on enemy ships in the scene
	
	public static int WaveNum = 1;
	
	// time passed since the player won
	public float TimeSinceWin{
		get { return Time.timeSinceLevelLoad - WinTimeStamp;}
	}
	// speed factor of the asteroid each level so the next level becomes harder
	public static float asteroidSpeedFactor{
		get{return 1+(0.05f*WaveNum);}	
	}
	// adds points to the game, the points added depends
	public static void AddPoints(int points){
		Scene.points += Mathf.RoundToInt(points *(1+0.1f*WaveNum)); 
	}
	
	private int[] asteroidWaves = {3, 5, 7, 10}; //number of asteroids per Wave 
	private int[] enemyAppearIntervalWave = {15, 13, 10, 7}; //interval appearance of enemy ships per Wave 
	
	public float timeForNewEnemy;

	public GUIStyle style = new GUIStyle();
	public GUIStyle styleGameOver = new GUIStyle();
	
	// Script initialization
	void Start () {
		if (lifes <= 0) {
			points = 0;
			lifes = maxLifes;
			GameIsOver = false;
		}
		if(Winner){
			points = 0;
			lifes = maxLifes;
			Winner = false;
			WaveNum = 1;	
		} 
		lifes--;
		WaveNum --;
		numberOfAsteroids = 0;
		numberOfEnemyShips = 0;
		timeForNewEnemy = enemyAppearIntervalWave[WaveNum];
		
	}
	
	void OnGUI() {
		Rect labelRect = new Rect(20, 20, Screen.width, Screen.height);
		GUI.Label(labelRect, points.ToString(), style);
		
		for (int i = 0; i <= lifes; ++i) {
			Rect lifePosition = new Rect(18 + 36 * i, 60, 30, 30);
			if(playerspaceship.textureNormal != null){
				GUI.DrawTexture(lifePosition, playerspaceship.textureNormal);
			}
		}
		
		labelRect.Set(20, 100, Screen.width, Screen.height);
		GUI.Label(labelRect, "Level: "+WaveNum, style);
		
		if (GameIsOver) {
			Rect label = new Rect(Screen.width / 2 - 140, Screen.height / 2, Screen.width, Screen.height);
			GUI.Label(label, "GAME OVER", styleGameOver);
		}
		
		if (Winner) {
			Rect label = new Rect(Screen.width / 2 - 140, Screen.height / 1.5f, Screen.width, Screen.height);
			GUI.Label(label, "YOU WON", styleGameOver);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(xa.paused) {
			return;
		}
		if (xa.isShoot && (GameIsOver || (Winner && TimeSinceWin > 1))) {
			Application.LoadLevel(Application.loadedLevel);
		}
		
		//the scene position should be the opposite of the spaceship
		//the rotation should be done on the "root" scene (in game Camera)
		//because the origin position changes
		this.transform.localPosition = -playerspaceship.originalPosition;
		if(GameIsOver || Winner) return;

		

		//creates ateroids if its theres not enought sprites
		if(numberOfAsteroids < 1){
			
			if(WaveNum  == asteroidWaves.Length){
				if(numberOfEnemyShips < 1){
					Winner= true;
					WinTimeStamp = Time.timeSinceLevelLoad;
				}
			} else {
			for(int i=0;i< asteroidWaves[WaveNum];++i){
				OTSprite sprite = RandomBlock(OT.view.worldRect, 0.9f, 1.8f, null,getRandomAsteroidSprite());
				sprite.transform.parent = this.transform;
				numberOfAsteroids++;
			}
			
			WaveNum++;
			}
		}
		
		// enemy ship appearance
		if(WaveNum <= enemyAppearIntervalWave.Length){
			timeForNewEnemy -= Time.deltaTime;
			if(timeForNewEnemy < 0){
				timeForNewEnemy += enemyAppearIntervalWave[WaveNum-1];
				OTSprite sprite = RandomBlock(OT.view.worldRect, 1.2f, 1.8f, null,"enemy");
				numberOfEnemyShips ++;
				sprite.transform.parent = this.transform;
			}
		}
		// player bullet shot
		if(xa.isShoot && !xa.shooting && !playerspaceship.isDead()){
			OTSprite bullet = OT.CreateSprite("bullet");
			bullet.transform.parent = transform;
			bullet.transform.localPosition = playerspaceship.originalPosition;
			Bullet bulletScript = bullet.GetComponent<Bullet>();
			if(bulletScript != null){
				bulletScript.initialize();
			}
		}
	}
	// splits asteroids in 2
	public static void SplitAsteroids(OTSprite original) {
		if (original.size.x > 0.9f) {
			var asteroid2 = OT.CreateSprite(getRandomAsteroidSprite());
			
			asteroid2.transform.parent = original.transform.parent;
			asteroid2.transform.localPosition = original.transform.localPosition;
			original.size = original.size / 2;
			asteroid2.size = original.size;
			numberOfAsteroids++;
		} else {
			numberOfAsteroids--;
			OT.DestroyObject(original);
		}
	}
	// creates an explosion on the sprite, removing them
	public static void ExplodeSprite(OTSprite sprite){
		OTSprite explosion = OT.CreateSprite("explosion");
		explosion.transform.parent = sprite.transform.parent;
		explosion.position = sprite.transform.localPosition;
		OT.DestroyObject(sprite);
	}
	
	public static void destroyEnemyShip(OTSprite sprite){
		ExplodeSprite(sprite);
		numberOfEnemyShips --;
	}
	
	// gets the prototype name of a random asteroid
	private static string getRandomAsteroidSprite()
	{
		return "asteroid" + (int)(1 + 3 * Random.value);;
	}
	
	OTSprite RandomBlock(Rect r, float min, float max, OTObject o,string property)
    {
        // Determine random size modifier (min-max)
        float size = min + Random.value * (max - min);
		OTSprite sprite = OT.CreateSprite(property);
        if (sprite != null)
        {
            // Set sprite's size
	        if (o != null)
	            sprite.size = o.size * size;
	        else
	            sprite.size = sprite.size * size;
			
            // Set sprite's random position
			switch((int)(3 * Random.value)) {
			case 0: // TOP
				sprite.position = new Vector2(r.xMin + Random.value * r.width, r.yMin);
				break;
			case 1: // RIGHT
				sprite.position = new Vector2(r.xMin + r.width, r.yMin + Random.value * r.height);
				break;
			case 2: // BOTTOM
				sprite.position = new Vector2(r.xMin + Random.value * r.width, r.yMin + r.height);
				break;
			case 3: // LEFT
				sprite.position = new Vector2(r.xMin, r.yMin + Random.value * r.height);
				break;
			}
			
            // Set sprite's random rotation
            sprite.rotation = Random.value * 360;
            // Set sprite's name
            /*sprite.depth = dp++;
            if (dp > 750) dp = 100;*/
        }
        return sprite as OTSprite;
    }
	
	
	
	
}

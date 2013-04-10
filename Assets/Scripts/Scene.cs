using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	
	public static int lifes = 3;
	public static int points;
	public static bool GameIsOver = false;
	public static bool Winner = false;
	public static int numberOfAsteroids = 0;
	public static int numberOfEnemyShips = 0;
	public static readonly int maxNumberOfLifes = 3;
	
	public static float enemyAppearInterval = 15f;
	public float timeForNewEnemy = enemyAppearInterval;
	
	
	private int[] asteroidWaves = {3,4,5,10}; //number of asteroids per Wave 
	private static int WaveNum = 1;
	public GUIStyle style = new GUIStyle();
	public GUIStyle styleGameOver = new GUIStyle();
	
	// Use this for initialization
	void Start () {
		if (lifes == 0) {
			points = 0;
			lifes = maxNumberOfLifes;
			GameIsOver = false;
		}
		if(Winner){
			lifes = maxNumberOfLifes;
			Winner = false;
			WaveNum = 1;	
		} 
		lifes--;
		WaveNum --;
		numberOfAsteroids = 0;
		timeForNewEnemy = enemyAppearInterval;
		
	}
	
	void OnGUI() {
		Rect labelRect = new Rect(20, 20, Screen.width, Screen.height);
		GUI.Label(labelRect, points.ToString(), style);
		
		for (int i = 0; i <= lifes; ++i) {
			Rect lifePosition = new Rect(18 + 36 * i, 60, 30, 30);
			GUI.DrawTexture(lifePosition, playerspaceship.textureNormal);
		}
		
		labelRect.Set(20, 100, Screen.width, Screen.height);
		GUI.Label(labelRect, "Wave: "+WaveNum, style);
		
		if (GameIsOver) {
			Rect label = new Rect(Screen.width / 2 - 140, Screen.height / 2, Screen.width, Screen.height);
			GUI.Label(label, "GAME OVER", styleGameOver);
		}
		
		if (Winner) {
			Rect label = new Rect(Screen.width / 2 - 140, Screen.height / 2, Screen.width, Screen.height);
			GUI.Label(label, "YOU WON", styleGameOver);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(xa.paused) {
			return;
		}
		if (xa.isShoot && (GameIsOver || Winner)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		
		if(GameIsOver || Winner) return;
		//the scene position should be the opposite of the spaceship
		//the rotation should be done on the "root" scene (in game Camera)
		//because the origin position changes
		this.transform.localPosition = -playerspaceship.originalPosition;
		timeForNewEnemy -= Time.deltaTime;
		if(timeForNewEnemy < 0){
			timeForNewEnemy += enemyAppearInterval;
			OTSprite sprite = RandomBlock(OT.view.worldRect, 0.9f, 1.8f, null,"enemy");
			sprite.transform.parent = this.transform;
			numberOfEnemyShips++;
		}
		//creates ateroids if its theres not enought sprites
		if(numberOfAsteroids < 1){
			
			if(WaveNum  == asteroidWaves.Length){
				if(numberOfEnemyShips < 1){
					Winner= true;
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

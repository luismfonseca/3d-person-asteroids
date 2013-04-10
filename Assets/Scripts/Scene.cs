using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	
	public static int lifes = 3;
	public static int points;
	public static bool GameIsOver = false;
	public GUIStyle style = new GUIStyle();
	public GUIStyle styleGameOver = new GUIStyle();
	
	// Use this for initialization
	void Start () {
		if (lifes == 0) {
			points = 0;
			lifes = 3;
			GameIsOver = false;
		}
		lifes--;
	}
	
	void OnGUI() {
		Rect labelRect = new Rect(20, 20, Screen.width, Screen.height);
		GUI.Label(labelRect, points.ToString(), style);
		
		for (int i = 0; i <= lifes; ++i) {
			Rect lifePosition = new Rect(18 + 36 * i, 60, 30, 30);
			GUI.DrawTexture(lifePosition, playerspaceship.textureNormal);
		}
		
		if (GameIsOver) {
			Rect label = new Rect(Screen.width / 2 - 140, Screen.height / 2, Screen.width, Screen.height);
			GUI.Label(label, "GAME OVER", styleGameOver);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(xa.paused) {
			return;
		}
		if (xa.isShoot && GameIsOver) {
			Application.LoadLevel(Application.loadedLevel);
		}
		//the scene position should be the opposite of the spaceship
		//the rotation should be done on the "root" scene (in game Camera)
		//because the origin position changes
		this.transform.localPosition = -playerspaceship.originalPosition;
		
		//creates ateroids if its theres not enought sprites
		if(OT.objectCount <= 1){
			//numRemainingWaves--
			//if(numRemainingWaves == 0){
			//	//you win		
			//}
			for(int i=0;i< 5;++i){
				OTSprite sprite = RandomBlock(OT.view.worldRect, 0.9f, 1.8f, null);
				sprite.transform.parent = this.transform;
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
		} else {
			OT.DestroyObject(original);
		}
	}
	
	private static string getRandomAsteroidSprite()
	{
		return "asteroid" + (int)(1 + 3 * Random.value);;
	}
	
	OTSprite RandomBlock(Rect r, float min, float max, OTObject o)
    {
        // Determine random 1-3 asteroid type
        int type = 1 + (int) (Random.value * 5);
        // Determine random size modifier (min-max)
        float size = min + Random.value * (max - min);
		OTSprite sprite = null;
        // Create a new asteroid
        switch (type)
        {
            case 1: sprite = OT.CreateSprite("asteroid1");
                break;
            case 2: sprite = OT.CreateSprite("asteroid2");
                break;
            case 3: sprite = OT.CreateSprite("asteroid3");
                break;
			case 4: sprite = OT.CreateSprite("asteroid4");
                break;
			case 5: sprite = OT.CreateSprite("enemy");
                break;
        }
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

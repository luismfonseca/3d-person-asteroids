using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(xa.paused) {
			return;
		}
		//the scene position should be the opposite of the spaceship
		//the rotation should be done on the "root" scene (in game Camera)
		//because the origin position changes
		this.transform.localPosition = -playerspaceship.originalPosition;
		
		//creates ateroids if its theres not enought sprites
		if(OT.objectCount <= 5){
			OTSprite sprite = RandomBlock(OT.view.worldRect, 0.6f, 1.8f, null);        
			sprite.transform.parent = this.transform;
		}
		if(xa.isShoot && !xa.shooting){
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
		if (original.size.x >= .5f) {
			var asteroid1 = OT.CreateSprite(getRandomAsteroidSprite());
			var asteroid2 = OT.CreateSprite(getRandomAsteroidSprite());
			
			asteroid1.transform.parent = original.transform.parent;
			asteroid1.transform.localPosition = original.transform.localPosition;
			asteroid2.transform.parent = original.transform.parent;
			asteroid2.transform.localPosition = original.transform.localPosition;
			asteroid1.size = original.size / 2;
			asteroid2.size = original.size / 2;
		}
		OT.DestroyObject(original);
	}
	
	private static string getRandomAsteroidSprite()
	{
		return "asteroid1";//;  + (1 + 3 * Random.value);
	}
	
	OTSprite RandomBlock(Rect r, float min, float max, OTObject o)
    {
        // Determine random 1-3 asteroid type
        int t = 1 + (int)Mathf.Floor(Random.value * 5);
        // Determine random size modifier (min-max)
        float s = min + Random.value * (max - min);
		OTSprite sprite = null;
        // Create a new asteroid
        switch (t)
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
	            sprite.size = o.size * s;
	        else
	            sprite.size = sprite.size * s;
            // Set sprite's random position
            sprite.position = new Vector2(r.xMin + Random.value * r.width, r.yMin + Random.value * r.height);
            // Set sprote's random rotation
            sprite.rotation = Random.value * 360;
            // Set sprite's name
            /*sprite.depth = dp++;
            if (dp > 750) dp = 100;*/
        }
        return sprite as OTSprite;
    }
	
	
	
	
}

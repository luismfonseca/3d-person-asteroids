using UnityEngine;
using System.Collections;

public class Scene : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
		//the scene position should be the opposite of the spaceship
		//the rotation should be done on the "root" scene (in game Camera)
		//because the origin position changes
		this.transform.localPosition = -playerspaceship.originalPosition;
		
		//creates ateroids if its theres not enought sprites
		if(OT.objectCount <= 12){
			OTSprite sprite = RandomBlock(OT.view.worldRect, 0.6f, 1.8f, null);        
			sprite.transform.parent = this.transform;
		}	
	}
	
	
	OTSprite RandomBlock(Rect r, float min, float max, OTObject o)
    {
        // Determine random 1-3 asteroid type
        int t = 1 + (int)Mathf.Floor(Random.value * 4);
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

using UnityEngine;
using System.Collections;

public class SceneSoundManager : MonoBehaviour {
	
	public static int currentLevelSound = 1;
	private static AudioClip[] levelBackgroundSounds;
	
	// Use this for initialization
	void Start() {
		levelBackgroundSounds = new AudioClip[4];
		for (int i = 0; i < levelBackgroundSounds.Length; i++) {
			levelBackgroundSounds[i] = Resources.Load("level" + (i + 1)) as AudioClip;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (currentLevelSound != Scene.WaveNum) {
			if (Scene.WaveNum == 0) // HACK
				return;
			currentLevelSound = Scene.WaveNum;
			audio.loop = false;
		}
		if (audio.time == 0) {
			audio.clip = levelBackgroundSounds[currentLevelSound - 1];
			audio.loop = true;
			audio.Play();
		}
	}
}

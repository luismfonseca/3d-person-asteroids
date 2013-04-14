using UnityEngine;
using System.Collections;

public class SceneSoundManager : MonoBehaviour {
	
	public static int currentLevelSound = 1;
	private static AudioClip[] levelBackgroundSounds;
	
	void Start() {
		levelBackgroundSounds = new AudioClip[4];
		for (int i = 0; i < levelBackgroundSounds.Length; i++) {
			levelBackgroundSounds[i] = Resources.Load("level" + (i + 1)) as AudioClip;
		}
	}
	
	void Update () {
		if (currentLevelSound != Scene.WaveNum) {
			if (Scene.WaveNum == 0) {
				return;
			}
			
			currentLevelSound = Scene.WaveNum;
			audio.loop = false;
		}
		if (audio.time == 0) {
			audio.clip = levelBackgroundSounds[currentLevelSound - 1];
			audio.loop = true;
			audio.Play();
		}
	}
	
	// Reference: http://answers.unity3d.com/questions/316575/adjust-properties-of-audiosource-created-with-play.html
	public static AudioSource PlayClipAt(AudioClip clip, Vector3 pos){
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		aSource.minDistance = 990099;
		// set other  properties here, if desired
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}
}

using UnityEngine;
using System.Collections;

public enum SoundClips {
    bluegrass,
    gobble1,
    gobble2,
    gobble3,
    gobble4,
    sliding,
    stonemans,
    turkeyScream,
	soundMax
}

public class SoundManager : MonoBehaviour {
	 
	public AudioClip[] mAudioClips =  new AudioClip[(int) SoundClips.soundMax];

	private static SoundManager instance;

	// Use this for initialization
	void Awake () {
        //DontDestroyOnLoad(this);
        instance = this;
		if (mAudioClips.Length != (int)SoundClips.soundMax) {
			Debug.Log("sound manager not synced !!");
		}
	}
	
    void Start()
    {
        AudioSource _source = playSound(SoundClips.stonemans, true);
        _source.volume = 0.09f;
    }
	// Update is called once per frame
	void Update () {
	
	}

	public static SoundManager getInstance(){
		return instance;
	}

	public AudioSource playSound(SoundClips clip, bool isLoop = false, bool autoDestroy = true) {
		AudioSource source = gameObject.AddComponent<AudioSource>();
		//source.PlayOneShot(mAudioClips [(int)clip]);
		source.clip = mAudioClips [(int)clip];
		if (isLoop) {
			source.loop = true;
		} else {
			if(autoDestroy)
				Destroy (source, mAudioClips [(int)clip].length + 1);
		}
		source.Play();
		return source;
	}

	public AudioSource playSound(SoundClips clip, AudioSource source) {

		if (source == null) {
			source = gameObject.AddComponent<AudioSource>();
			source.PlayOneShot(mAudioClips [(int)clip]);
			Destroy(source,mAudioClips [(int)clip].length + 1);
		} else {
			source.PlayOneShot (mAudioClips [(int)clip]);
		}

		return source;
	}

	public void updatePitch() {
		AudioSource[] tempList = GetComponents<AudioSource> ();

		foreach(AudioSource item in tempList)
		{
			item.pitch = (Time.timeScale < 0.5f) ? 0.2f : 1f;
		}
	}

	public int currentlyPlayingSounds () {
		AudioSource[] tempList = GetComponents<AudioSource> ();
		return tempList.Length;
	}

	public void stopAllMusic () {
		AudioSource[] tempList = GetComponents<AudioSource> ();
		
		foreach(AudioSource item in tempList)
		{
			item.Stop();
		}
	}
}

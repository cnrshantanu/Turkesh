using UnityEngine;
using System.Collections;

public class VideoScript : MonoBehaviour {
	MovieTexture _movie;

	// Use this for initialization
	void Start () {
		_movie = gameObject.GetComponent<Renderer>().material.mainTexture as MovieTexture;
		_movie.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (!_movie.isPlaying && Application.loadedLevelName == "LoseScene") {
			StartCoroutine(loadLevel("Final Time"));
		}	

        if (Application.loadedLevelName == "LoseScene")
        {
            if (Input.anyKey)
            {
                Application.LoadLevel("House");
            }
        }
        if (Application.loadedLevelName == "House")
        {
            Application.LoadLevel("WinScene");

        }
    }

    IEnumerator loadLevel (string name) {
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel(name);
	}
}

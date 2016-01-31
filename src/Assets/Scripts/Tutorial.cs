using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Disable", 5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Disable()
    {
        this.gameObject.SetActive(false);
    }
}

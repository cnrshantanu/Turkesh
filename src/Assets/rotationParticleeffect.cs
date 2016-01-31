using UnityEngine;
using System.Collections;

public class rotationParticleeffect : MonoBehaviour {

	public float angle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		var rot = transform.eulerAngles;
		rot.y+= angle;
		transform.eulerAngles = rot;
	}
}

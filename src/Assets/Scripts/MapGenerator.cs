using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public GameObject planeGameObject;

	// Use this for initialization
	void Start () {

		for (int i = 0; i < 40; i = i + 10) {
			for (int j = 0; j < 40; j = j + 10) {
				Quaternion quatAngle = new Quaternion();
				quatAngle.eulerAngles = new Vector3 (0, 0, 0);
				GameObject tileGameObject = Instantiate(planeGameObject, new Vector3(i, 0, j), quatAngle) as GameObject;
				tileGameObject.name = i + " " + j;
				tileGameObject.transform.parent = GameObject.Find("Tiles").transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

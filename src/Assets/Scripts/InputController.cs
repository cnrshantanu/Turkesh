using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {

	// Use this for initialization
	//float [] wingspeed;
	float angle = 0;
    public GameObject Turkesh;
	public List<float> wingspeed = new List<float>(){
		0,
		0
	};
	public float movementSpeed = 1;
	public float accuracy = 0.1f;

	void Start () {
		wingspeed.Add (0);
		wingspeed.Add (0);
		wingspeed [0] = Input.GetAxis ("Vertical1");
		wingspeed [1] = Input.GetAxis ("Vertical2");
	}

	// Update is called once per frame
	void Update () {
		float left = Input.GetAxis ("Vertical1") - wingspeed[0];
		float right = Input.GetAxis ("Vertical2") - wingspeed[1];

		Vector3 fwd = transform.TransformDirection(0,Mathf.Clamp((left-right),-90,90),0);

			angle = left-right;
			angle = angle*-((Turkesh.transform.rotation.y)+20);

		if(Input.GetAxis ("Vertical1") - wingspeed[0]>accuracy || Input.GetAxis ("Vertical2") - wingspeed[1] >accuracy){
			//Debug.Log (angle);
			var rot = Turkesh.transform.eulerAngles;
			rot.y+= angle;
			Turkesh.transform.eulerAngles = rot;
		}
		Turkesh.transform.position = Vector3.Lerp(Turkesh.transform.position,transform.position + transform.forward* (Mathf.Abs((left + right) / 2))*movementSpeed,0.5f);
		wingspeed [0] = Input.GetAxis ("Vertical1");
		wingspeed [1] = Input.GetAxis ("Vertical2");
		
		TurkeyMover mover = GetComponent<TurkeyMover>();
		int movement;
		if (left - right > accuracy) {
			movement =1;

		} else if (right - left > accuracy) {
			movement = 2;

		} else {
			movement =3;

		}
		mover.Move(movement);

	}
}

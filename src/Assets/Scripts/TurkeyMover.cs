using UnityEngine;
using System.Collections;

public class TurkeyMover : MonoBehaviour {

	// Use this for initialization

	public int movement;
	private Animator m_animationController;
	private static TurkeyMover instance;

	void Start () {
		m_animationController = GetComponent<Animator>();
		instance = this;
		movement = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			Move(1);
		}else if (Input.GetKeyDown (KeyCode.D)) {
			Move(2);
		}else if (Input.GetKeyDown (KeyCode.W)) {
			Move(3);
		}
	}

	public static TurkeyMover getInstance() {
		return instance;
	}

	public void Move(int _movement) {
		switch (_movement) {
		case 1:
			m_animationController.SetBool("left",true);
			m_animationController.SetBool("right",false);
			break;
		case 2:
			m_animationController.SetBool("left",false);
			m_animationController.SetBool("right",true);
			break;
		case 3:
			m_animationController.SetBool("left",true);
			m_animationController.SetBool("right",true);
			break;
		}
	}
}

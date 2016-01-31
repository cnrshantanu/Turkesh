using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestMovementScript : MonoBehaviour {

    bool _increaseSpeed = false;
	private AudioSource m_ASlide;
	public float speed;
    public int _countDown;
    public int _numberOfCol = 0;
    public Text _text;

	// Use this for initialization
	void Start () {
		StartCoroutine (RandomSounds ());
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(Vector3.forward * Time.deltaTime);
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position += transform.TransformDirection (Vector3.forward) * Time.deltaTime * 4f;
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y-(Time.deltaTime * 90f),transform.eulerAngles.z);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y+(Time.deltaTime* 90f),transform.eulerAngles.z);
		}

        // Increase Speed
        if (_increaseSpeed)
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

        // Win Sequence
        if (transform.position.y < 0.0f && transform.position.y > -1.0f) {
            Application.LoadLevel("WinScene");
        }
    }

	IEnumerator RandomSounds() {
		while (true) {
			yield return new WaitForSeconds (Random.Range (2, 5));
			AudioSource _audio = SoundManager.getInstance ().playSound ((SoundClips)Random.Range ((int)SoundClips.gobble1, (int)SoundClips.gobble4));
			_audio.volume = 0.2f;
		}
	}
    void OnCollisionEnter (Collision col)
    {
        if (!_increaseSpeed && col.gameObject.tag == "Villain")
        {
            _numberOfCol++;
            _countDown = (int)Time.time;
            //Application.LoadLevel("LoseScene");
        }
        // Villain Collision
        if (_increaseSpeed && col.gameObject.tag == "Villain")
        {
            Debug.Log("Force!");
            //gameObject.GetComponent<Rigidbody>().isKinematic = true;
            col.gameObject.GetComponent<NavMeshAgentScript>().forceAdded = true;
            //gameObject.GetComponent<Rigidbody>().mass = 100.0f;
            //col.gameObject.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward));
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (!_increaseSpeed && col.gameObject.tag == "Villain")
        {
            _numberOfCol--;
            if (_numberOfCol == 0)
                _text.text = "";
            //Application.LoadLevel("LoseScene");
        }
        

    }

    void OnCollisionStay(Collision col)
    {
       if(_numberOfCol > 0)
        {
            _text.text = ((int)(4 - (Time.time - _countDown))).ToString();
            if(Time.time - _countDown > 3 )
            {
                Application.LoadLevel("LoseScene");
            }
        }
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Splat")
        {
            _increaseSpeed = true;
            if(m_ASlide == null)
				m_ASlide = SoundManager.getInstance().playSound(SoundClips.sliding);

            gameObject.GetComponentInChildren<Animator>().SetTrigger("slide");
            Invoke("returnToWalkingAnimation", 3.0f);
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Villain")
        {
            col.gameObject.GetComponent<NavMeshAgentScript>().forceAdded = false;
        }
        gameObject.GetComponent<Rigidbody>().mass = 1.0f;
    }

    // Return to the walking animation
    void returnToWalkingAnimation ()
    {
        _increaseSpeed = false;
        gameObject.GetComponentInChildren<Animator>().SetTrigger("walk");
    }
}

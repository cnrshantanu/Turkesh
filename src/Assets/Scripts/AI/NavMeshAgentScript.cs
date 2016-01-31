using UnityEngine;
using System.Collections;

public class NavMeshAgentScript : MonoBehaviour {

    public Transform turkey;
    public Transform[] patrolPoints;
    public int destPoints = 0;
    NavMeshAgent _agent;
    public bool patrolAI;
    public bool forceAdded = false;
    public GameObject particleSystem;
	public static AudioSource m_Audio = null;

    void GoToNextPointInArray()
    {
        if (patrolPoints.Length == 0)
            return;
        _agent.destination = patrolPoints[destPoints].position;
        transform.LookAt(patrolPoints[destPoints].position);
        destPoints = (destPoints + 1) % patrolPoints.Length;
    }
	// Use this for initialization
	void Start () {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
        _agent.speed=(float)Random.Range(10,18)/10f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(turkey.position, gameObject.transform.position) < 3f)
        {
            //Produce fork animation
			if(m_Audio == null)
				m_Audio = SoundManager.getInstance().playSound(SoundClips.turkeyScream);

            patrolAI = false;
        }
        if (!patrolAI)
        {
            _agent.SetDestination(turkey.position);
            gameObject.transform.LookAt(turkey.position);
        }
        else
        {
            if (_agent.remainingDistance < 0.5f)
                GoToNextPointInArray();
        }

        // Force
        if (forceAdded)
        {
            Debug.Log("H!!!");
            forceAdded = false;
            //_agent.enabled = false;



            GameObject particleObject = Instantiate(particleSystem, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
            Destroy(particleObject, 2.0f);
            Destroy(gameObject);

            //InvokeRepeating("forceAddedMethod", 0.1f, 0.1f);
            //StartCoroutine(forceAddedMethod());
            //gameObject.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward * -10), ForceMode.Force);
        }
	}

    IEnumerator forceAddedMethod ()
    {
        float currentTime = Time.time;

        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 2.0f);

        while (Time.time - currentTime < 0.3f)
        {
            Debug.Log("Forceddddd");
            //gameObject.transform.Translate(Vector3.forward * -10 * Time.deltaTime);
            yield return 1;
        }
        CancelInvoke("forceAddedMethod");
        _agent.enabled = true;
    }
}

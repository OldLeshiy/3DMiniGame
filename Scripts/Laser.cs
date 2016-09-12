using UnityEngine;
using System.Collections;

// Лазер(создание луча, попадания)

public class Laser : MonoBehaviour {
	
	public Transform attackPoint;
	public Transform ray;
	private Transform playerPoint;
	private LineRenderer line;
	private AudioSource laserSourse;
	public AudioClip aclipLaser; 
	private float timefire;

	void Start ()
	{
		timefire = 0f;
		laserSourse = GetComponent<AudioSource>();
		attackPoint = GetComponent<Transform> ();
		line = GetComponent<LineRenderer> ();
		ray = GetComponent<Transform> ();
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			PlayLaserAudio ();
		}
		if (Input.GetMouseButtonUp(0))
		{
			laserSourse.Stop();
			line.SetPosition (1, new Vector3 (0, 0, 0));
		}
		if (Input.GetMouseButton (0))
		{	
			KillSkeleton ();	
		} 
	}

	void KillSkeleton()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast (ray.position, ray.up, out hitInfo))
		{	
			if (hitInfo.collider) {
				if (hitInfo.collider.tag == "Enemy") {
					timefire += Time.deltaTime;
					if (timefire >= 3f) {
						hitInfo.collider.gameObject.GetComponent<EnemyController> ().deathInit = true;
						hitInfo.collider.gameObject.GetComponent<EnemyController> ().Die ();
						timefire = 0f;
					}
				} else
					timefire = 0f;
				line.SetPosition (1, new Vector3 (0, hitInfo.distance*1.2f, 0));
			}
		}
		else
		{
			line.SetPosition (1, new Vector3 (0, 50000, 0));
		}
	}

	void PlayLaserAudio ()
	{
		laserSourse.clip = aclipLaser;
		laserSourse.Play();
	}

}

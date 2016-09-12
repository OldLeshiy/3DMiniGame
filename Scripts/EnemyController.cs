using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Контроллер монстров(движение, атака, смерть )

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2;
    public float turnSpeed = 90;
	private float _timeout=0;
	private float timeout=5.18f;
	private float tout=0f;
	private GameObject player;
	private GameObject button;
	public GameObject explosion;
	public GameObject rPoint;
	private Animator animator;
	private CharacterController _controller;
    private Transform _thisTransform;
    private Transform _playerTransform;
	private Transform rayPoint;
	private Renderer colorButton;
	private bool danger,newdanger;
	private bool turn;
	public bool deathInit;
	private bool stop;
	private bool exp;

	void Start()
    {
		button = GameObject.Find ("Button");
		player = GameObject.Find ("FPSController");
		_controller = GetComponent<CharacterController>();
        _thisTransform = transform;
		player = GameObject.FindGameObjectWithTag ("Player");
        _playerTransform = player.transform;
		animator = GetComponent<Animator> ();
		colorButton = button.gameObject.GetComponent<Renderer> ();
		rayPoint= rPoint.transform;
		danger = false;
		newdanger = false;
		turn = true;
		deathInit = false;
		stop = false;
		exp = false;
    }

	void Update()
	{
		if (colorButton.material.color == Color.green) {
			danger = true;
		} else
			danger = false;
	}
	void FixedUpdate()
    {
		if (danger || newdanger) {
			Attack ();	
		} else
			Patrol ();
			_controller.Move (Vector3.down * 10.0f * Time.deltaTime);

		if (deathInit) {
			stop = true;
			Die();
		}
    }
	void Attack()
	{
		if (!stop) {
			Vector3 playerDirection = (_playerTransform.position - _thisTransform.position).normalized;
			float angle = Vector3.Angle (_thisTransform.forward, playerDirection);
			float maxAngle = turnSpeed * Time.deltaTime;
			Quaternion rot = Quaternion.LookRotation (_playerTransform.position - _thisTransform.position);
			if (maxAngle < angle) {
				_thisTransform.rotation = Quaternion.Slerp (_thisTransform.rotation, rot, maxAngle / angle);
			} else {
				_thisTransform.rotation = rot;
			}

			if (Vector3.Distance (_playerTransform.position, _thisTransform.position) > 2f &&
			   Vector3.Distance (_playerTransform.position, _thisTransform.position) < 15.0f) {
				animator.Play ("Walk");
				_controller.Move (_thisTransform.forward * moveSpeed * Time.deltaTime * .5f);
			}

			if (Vector3.Distance (_playerTransform.position, _thisTransform.position) <= 2f) {
				
				animator.Play ("Attack");
			}
		}
	}
	void Patrol()
	{
		if (!stop) {
			RaycastHit hit;
			if (Physics.Raycast (rayPoint.position, rayPoint.forward, out hit)) {
				if (hit.collider.tag != "Player") {
					if (hit.distance > 3 || turn) {
						_controller.Move (_thisTransform.forward * moveSpeed * Time.deltaTime);
						animator.Play ("Walk");
						turn = false;
					} else if (hit.distance <= 3 || !turn) {
						_timeout += Time.deltaTime;
						animator.Play ("Idle");
						if (_timeout >= timeout) {
							float grad = Random.Range (-180f, 180f);
							Quaternion angleRotMonstr = Quaternion.Euler (0f, grad, 0f);
							_thisTransform.rotation = Quaternion.Slerp (_thisTransform.rotation, angleRotMonstr, 1f);
							turn = true;
							_timeout = 0;
						}
					}
			
				} else
					newdanger = true;
			}
		}
	}
	public void Die ()
	{
		tout += Time.deltaTime;
		if (tout < 1.5f || deathInit) {
			animator.Play ("Death");
			if (!exp) {
				GameObject explosionClone=(GameObject) Instantiate (explosion, transform.position, transform.rotation);
				exp = true;
				DestroyObject (explosionClone,1.5f);
			}
		}

		if (tout > 1.8f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter(Collider myCollision) {
		if (myCollision.gameObject.tag == "Player")
		{
			SceneManager.LoadScene("MenuLoser");
		}
	}
}
 
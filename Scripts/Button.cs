using UnityEngine;
using System.Collections;

// отвечает за нажатие кнопки открывания двери

public class Button : MonoBehaviour {

	private bool pressButton;
	public GameObject ocDoor;
	public GameObject player;
	private double _timeout=2;
	private double timeout=2;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		pressButton = false;
		gameObject.GetComponent<Renderer> ().material.color = Color.red;
	}
	
	void Update ()
	{
		_timeout += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.E) & _timeout>timeout & Vector3.Distance(transform.position,player.transform.position)<2) 
		{
			pressButton = !pressButton;
			_timeout = 0;
			if (pressButton) {
				gameObject.transform.position += new Vector3 (0, 0, 0.05f);
				gameObject.GetComponent<Renderer> ().material.color = Color.green; 
			}
			else
			{
				gameObject.transform.position += new Vector3(0, 0, -0.05f);
				gameObject.GetComponent<Renderer> ().material.color = Color.red;
			}
			ocDoor.gameObject.GetComponent<Door>().DoOpenClose();
		}
	}
	void OnGUI()
	{
		if (Vector3.Distance(transform.position,player.transform.position)<2)
		{
			GUI.TextField (new Rect (Screen.width / 2-28, Screen.height / 2-10, 56, 20), "Press E");
		}
	}
}

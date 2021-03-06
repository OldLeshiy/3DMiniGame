using UnityEngine;
using System.Collections;

// Открытие, закрытие двери

public class Door : MonoBehaviour {

	private bool open;
	private bool completed;
	private Animation anim;

	void Start()
	{
		anim = GetComponent<Animation> ();
		completed = true;
	}
	void Update ()
	{
		if (!completed)
		{
			open = !open;
			completed = true;
			if (open == true) 
			{
				anim.Play ("open");
			}
			else
			{
				anim.Play ("close");
			}
		}
	}
	public void DoOpenClose()
	{
		completed = false;
	}
}

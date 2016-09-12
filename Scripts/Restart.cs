using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// перезагрузка игры

public class Restart : MonoBehaviour {

	void Update()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	public void ClickToRestart()
	{
		SceneManager.LoadScene("taskHotels");
	}
}

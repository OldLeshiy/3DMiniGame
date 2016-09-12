using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//вызов меню победителя, проверка наличия монстров

public class Winner : MonoBehaviour {

	public GameObject enemy;
	private GameObject[] arrayOfEnemy;
	private float timeSeek;

	void Start ()
	{
		timeSeek = 0f;
	}
	
	void Update ()
	{
		timeSeek += Time.deltaTime;
		if (timeSeek >= 5f) {
			arrayOfEnemy = GameObject.FindGameObjectsWithTag ("Enemy");
			int count = arrayOfEnemy.Length;
			if (count == 0)
				SceneManager.LoadScene ("MenuWinner");
			timeSeek = 0f;
		}
	}
}


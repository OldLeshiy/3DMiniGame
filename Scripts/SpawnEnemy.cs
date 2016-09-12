using UnityEngine;
using System.Collections;

//создание монстров на карте из префаба

public class SpawnEnemy : MonoBehaviour {

	public GameObject skeleton;
	private int countSkelet = 3;
	private float radius = 3f;

	void Start () {
		for (int i=1;i<=countSkelet;i++)
		{
			float angle = i * Mathf.PI * 2 / countSkelet;
			Vector3 pos = new Vector3(Mathf.Cos(angle)+3f, 0, Mathf.Sin(angle)) * radius;
			Instantiate(skeleton, pos, Quaternion.identity);
		}
	}
}

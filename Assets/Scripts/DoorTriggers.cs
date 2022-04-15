using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorTriggers : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (gameObject.CompareTag("Easy"))
			Data.LevelOfDifficulty = 0;
		else if (gameObject.CompareTag("Normal"))
			Data.LevelOfDifficulty = 1;
		else if (gameObject.CompareTag("Hard"))
			Data.LevelOfDifficulty = 2;

		StartCoroutine(LoadMainScene());
	}

	IEnumerator LoadMainScene()
	{
		yield return new WaitForSeconds(2.0f);
	
		SceneManager.LoadScene("Main");
	}
}

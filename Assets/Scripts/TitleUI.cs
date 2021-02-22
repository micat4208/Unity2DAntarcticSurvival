using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
	private void Update()
	{
		// 엔터 키가 눌렸다면
		if (Input.GetKeyDown(KeyCode.Return))
		{
			GameManager.gameManager.isGameOver = false;

			// 게임 씬으로 전환합니다.
			SceneManager.LoadScene("GameScene");
		}
	}

}

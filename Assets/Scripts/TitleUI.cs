using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{

	[Header("최고 점수 기록 시간 텍스트")]
	public Text m_RecordTimeText;

	[Header("최고 점수 텍스트")]
	public Text m_BestScoreText;

	[Header("안드로이드용 게임 시작 버튼")]
	public Button m_ANDROID_GameStartButton;


	private void Start()
	{
		m_RecordTimeText.text = GameManager.gameManager.bestData.ToDateString();
		m_BestScoreText.text = GameManager.gameManager.bestData.bestScore.ToString("0.00");

#if UNITY_ANDROID
		// 안드로이드라면
		// 버튼 이벤트를 설정합니다.
		m_ANDROID_GameStartButton.onClick.AddListener(StartGame);

#else
		// 안드로이드가 아니라면 버튼 비활성화
		m_ANDROID_GameStartButton.gameObject.SetActive(false);
#endif

	}

	private void Update()
	{
		// 엔터 키가 눌렸다면 게임 시작
		if (Input.GetKeyDown(KeyCode.Return))
			StartGame();
	}

	// 게임을 시작합니다.
	private void StartGame()
	{
		// 게임 오버 상태 초기화
		GameManager.gameManager.isGameOver = false;

		// 점수 초기화
		GameManager.gameManager.ClearScore();

		// 게임 씬으로 전환합니다.
		SceneManager.LoadScene("GameScene");
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	// UI 의 펭귄 이미지가 이동할 수 있는 영역을 나타내는 RectTransform
	[Header("UI 펭귄 이미지 Bounds")]
	public RectTransform m_LifeGaugeBounds;

	// UI 의 펭귄 이미지를 나타냅니다.
	[Header("UI 펭귄 이미지")]
	public RectTransform m_CurrentStateImage;

	[Header("점수를 표시하는 텍스트")]
	public Text m_ScoreText;

	// GameOver Panel Prefab 을 나타냅니다.
	public RectTransform m_GameOverPanelPrefab;

	private PlayerCharacter _PlayerCharacter;
	private RectTransform _GameOverTransform;


	private void Start()
	{
		_PlayerCharacter = GameManager.gameManager.playerCharacter;
	}

	private void Update()
	{
		// 펭귄 이미지의 위치를 계산합니다.
		{
			// 현재 위치를 저장합니다.
			Vector2 newPosition = m_CurrentStateImage.anchoredPosition;
			/// - RectTransform.anchoredPosition : UI 요소 위치 계산시 Anchor 값의 영향을 받는 위치를 얻습니다.

			// 좌측을 0% 우측을 100% 로 보이도록 하기 위해 펭귄 이미지의 X 위치를 변경합니다.
			newPosition.x = (m_LifeGaugeBounds.sizeDelta.x) * (_PlayerCharacter.hungry * 0.01f);
			/// - RectTransform.sizeDelta : 해당 UI 요소의 가로 세로 크기를 Vector2 형식으로 반환합니다.

			// 계산한 위치를 적용시킵니다.
			m_CurrentStateImage.anchoredPosition = newPosition;
		}

		// 플레이어 캐릭터의 사망을 확인합니다.
		if (GameManager.gameManager.isGameOver)
			// 사망했다면 GameOver
			CreateGameOverUI();

		UpdateCurrentScore();
	}

	private void CreateGameOverUI()
	{
		if (_GameOverTransform) return;

		_GameOverTransform = Instantiate(m_GameOverPanelPrefab);
		_GameOverTransform.SetParent(transform);
		/// - SetParent() 부모 오브젝트를 설정합니다.

		// RectTransform 의 left, top, right, bottom 을 설정합니다.
		_GameOverTransform.offsetMin =
		_GameOverTransform.offsetMax = Vector2.zero;

		// 스케일을 설정합니다.
		_GameOverTransform.localScale = Vector3.one;

		// 앵커를 설정합니다.
		_GameOverTransform.anchorMin = Vector2.zero;
		_GameOverTransform.anchorMax = Vector2.one;

		// 코루틴
		/// - 어떠한 작업을 동시에 처리할 때 시간 간격을 두고 작업을 처리할 수 있도록
		///   도와주는 함수 형식
		/// - IEnumerator 형식을 리턴시켜야 하며, yield 키워드로 함수의 종료 지점을
		///   설정합니다.
		/// - StartCoroutine() 으로 코루틴을 실행시키며
		///   StopCoroutine() 으로 코루틴을 종료합니다.
		///   
		/// - yield return new WaitWhile(Func<bool> predicate)
		///   predicate 가 거짓을 반환할 때까지 대기합니다.
		///   
		/// - yield return new WaitUntil(Func<bool> predicate)
		///   predicate 가 참을 반환할 때까지 대기합니다.
		///   
		/// - yield return new WaitForSeconds(float time)
		///   time 이 지날 때까지 대기합니다.
		///   Timescale 의 영향을 받습니다.
		///   
		/// - yield return new WaitForSecondsRealtime(float time)
		///   time 이 지날 때까지 대기합니다.
		///   Timescale 의 영향을 받지 않습니다.
		///   
		/// - yield return null or 0;
		///   Update 호출까지 대기합니다.
		///   
		/// - yield break;
		///   실행중인 코루틴을 종료합니다.
		
		// 3 초를 대기합니다.
		IEnumerator Wait3Sec()
		{
			// 3 초를 대기합니다.
			yield return new WaitForSeconds(3.0f);

			// Title Scene 으로 전환시킵니다.

			SceneManager.LoadScene("TitleScene");
			/// - SceneManager : 씬과 관련된 정적 메서드를 제공하는 제공하는 클래스
			/// - LoadScene(sceneName) : sceneName 과 일치하는 씬을
			///   동기 방식으로 로드합니다.
		}

		// 코루틴 실행
		StartCoroutine(Wait3Sec());
	}

	// 현재 점수를 갱신합니다.
	private void UpdateCurrentScore()
	{
		m_ScoreText.text = 
			$"현재 점수\n{GameManager.gameManager.currentScore.ToString("0.00")}";
	}


}

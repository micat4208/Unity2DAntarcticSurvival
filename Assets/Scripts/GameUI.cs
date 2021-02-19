using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
	// UI 의 펭귄 이미지가 이동할 수 있는 영역을 나타내는 RectTransform
	[Header("UI 펭귄 이미지 Bounds")]
	public RectTransform m_LifeGaugeBounds;

	// UI 의 펭귄 이미지를 나타냅니다.
	[Header("UI 펭귄 이미지")]
	public RectTransform m_CurrentStateImage;

	private PlayerCharacter _PlayerCharacter;

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


	}



}

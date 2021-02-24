using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AndroidMoveInput : MonoBehaviour,
	IPointerDownHandler, IPointerUpHandler
/// - IPointerDownHandler : 클릭 / 터치 입력 콜백을 받기 위해 구현하는 인터페이스
/// - IPointerUpHandler : 클릭 / 터치 입력 끝 콜백을 받기 위해 구현하는 인터페이스
{
	public bool isInput { get; private set; }

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData) =>
		isInput = true;

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData) => 
		isInput = false;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
	[Header("오브젝트 타입")]
	public FallingObjectType FallingObjType;

	[Header("배고픔 게이지 변경 값")]
	[Range(0.0f, 50.0f)]
	public float m_ChangeHungryValue = 0.0f;


	// 해당 오브젝트가 소유하는 FallingMovement 컴포넌트에 대한 프로퍼티입니다.
	public FallingMovement fallingMovement { get; private set; }

	private void Awake()
	{
		fallingMovement = GetComponent<FallingMovement>();
		/// - GetComponent<ComponentType>() : 자신을 소유하는 오브젝트 내에서 ComponentType 과 일치하는
		///   컴포넌트를 찾아 반환합니다.
		/// - 만약 찾지 못했을 경우 null 을 반환합니다.

		// 물고기 타입의 오브젝트라면 Roll 회전을 사용하지 않도록 합니다.
		fallingMovement.useRollRotation = 
			(FallingObjType == FallingObjectType.Fish) ? false : true;
	}

	// 겹침 처리에 사용됩니다.
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 플레이어 캐릭터와 겹쳤을 경우
		/// - "Player" 태그를 갖는 오브젝트와 겹쳤을 경우
		if (collision.CompareTag("Player"))
		{
			PlayerCharacter playerCharacter = GameManager.gameManager.playerCharacter;
				// collision.gameObject.GetComponent<PlayerCharacter>();

			// 캐릭터의 배고픔 값을 변경합니다.
			playerCharacter.ChangeHungryValue(
				(FallingObjType == FallingObjectType.Fish) ?
				m_ChangeHungryValue : 
				-m_ChangeHungryValue);

			// 겹친 이후 해당 오브젝트를 제거합니다.
			Destroy(gameObject);
		}

	}

}

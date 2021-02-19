using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFallingObject : MonoBehaviour
{
	[Header("물고기 오브젝트들")]
	public FallingObject[] m_FishObjects;

	[Header("쓰레기 오브젝트들")]
	public FallingObject[] m_TrashObjects;

	[Header("물고기가 떨어질 확률")]
	[Range(1.0f, 100.0f)]
	public float _FishDropPercentage = 10.0f;
	/// - Range(min, max) : 인스펙터 창에 min 부터 max 까지 값을 설정할 수 있는 슬라이더를 표시합니다.
	// 해당 확률에 따라 물고기 오브젝트가 생성될 수 있도록 해보세요!


	// 하늘에서 떨어지도록 할 오브젝트를 생성하는 딜레이
	[Header("오브젝트 생성 최소, 최대 딜레이")]
	// 최소 딜레이
	public float m_MinDelay = 1.0f;
	// 최대 딜레이
	public float m_MaxDelay = 0.01f;
	// 현재 딜레이
	public float m_Delay = 1.0f;

	// 마지막으로 오브젝트를 생성한 시간을 저장할 변수
	public float _LastDropTime;


	private void Start()
	{
		m_Delay = m_MinDelay;
	}

	private void Update()
	{
		// 떨어지는 오브젝트 생성
		CreateFallingObject();

		// 생성 딜레이 점점 더 빠르게
		ChangeDelayFaster();
	}

	// 생성 딜레이를 점점 더 빠르게 변경합니다.
	private void ChangeDelayFaster()
	{
		m_Delay = (m_Delay < m_MaxDelay) ?
			m_MaxDelay :
			m_Delay - (Time.deltaTime * 0.02f);
	}

	// 떨어지는 오브젝트를 생성합니다.
	private void CreateFallingObject()
	{
		// 랜덤한 위치에 오브젝트를 생성합니다.
		/// - objType : 어떤 타입의 오브젝트를 생성할 것인지를 전달합니다.
		void RandomSpawn(FallingObjectType objType)
		{
			// 생성된 오브젝트를 참조할 변수
			FallingObject newFallingObject = null;

			switch (objType)
			{
				case FallingObjectType.Trash:
					newFallingObject = Instantiate(m_TrashObjects[Random.Range(0, m_TrashObjects.Length)]);
					/// - Instantiate<T>(T original) : original 객체를 복사 생성하여 T 형식으로 반환합니다.
					///   T -> GameObject : 해당 오브젝트를 복사 생성합니다.
					///   T -> Component : Component 를 소유하는 오브젝트를 복사 생성하고, 
					///        복사 생성된 오브젝트의 T 형식의 컴포넌트를 반환합니다.
					break;

				case FallingObjectType.Fish:
					newFallingObject = Instantiate(m_FishObjects[Random.Range(0, m_FishObjects.Length)]);
					break;
			}

			// 떨어뜨릴 오브젝트에 설정될 위치를 저장할 변수
			Vector3 dropPosition = transform.position;
			/// - Spawner 의 위치를 저장합니다.

			// 랜덤한 위치를 설정합니다.
			dropPosition.x =
				Random.Range(PlayerCharacter.MaxLeftPositionX, PlayerCharacter.MaxRightPositionX);

			// 떨어뜨릴 오브젝트 위치에 설정된 위치를 대입합니다.
			newFallingObject.transform.position = dropPosition;
		}

		if (Time.time - _LastDropTime >= m_Delay)
		{
			_LastDropTime = Time.time;
			/// - Time.time : 게임이 실행된 후 지난 시간을 초단위로 반환합니다.

			RandomSpawn(Random.Range(1.0f, 100.0f) <= _FishDropPercentage ? 
				FallingObjectType.Fish : FallingObjectType.Trash);

		}
	}

}

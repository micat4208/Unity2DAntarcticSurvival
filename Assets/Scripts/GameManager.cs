using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// 게임 전체의 데이터를 관리할 클래스입니다.
public class GameManager : MonoBehaviour
{
	// GameManager 객체를 참조할 변수입니다.
	private static GameManager _GameManagerInstance;
	public static GameManager gameManager => GameManagerInitialize();

	// PlayerCharacter 객체를 참조할 변수입니다.
	private PlayerCharacter _PlayerCharacterInstance;
	public PlayerCharacter playerCharacter => _PlayerCharacterInstance;


	private void Awake()
	{
		if (_GameManagerInstance == null) GameManagerInitialize();

		// GameManager 중복 방지
		else if (_GameManagerInstance != this)
		{
			Destroy(gameObject);
			return;
		}

		// 해당 컴포넌트를 갖는 게임 오브젝트가 씬이 교체되어도 사라지지 않도록 합니다.
		DontDestroyOnLoad(gameObject);
	}

	private static GameManager GameManagerInitialize()
	{
		return _GameManagerInstance = _GameManagerInstance ??
			GameObject.Find("GameManager").GetComponent<GameManager>();
		/// - GameObject.Find(objName) : objName 과 일치하는 이름을 가진 오브젝트를
		///   월드에서 찾아 반환합니다.
	}

	public void SetPlayerCharacter(PlayerCharacter playerCharacter)
	{
		this._PlayerCharacterInstance = playerCharacter;
	}

	

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

	// 게임 오버를 나타냅니다.
	public bool isGameOver { get; set; }

	// 최고 점수 데이터를 나타냅니다.
	public BestDataInfo bestData { get; private set; }

	// 현재 점수를 나타냅니다.
	public double currentScore { get; private set; }

	// Json 파일을 저장하기 위한 경로를 저장할 변수
	private string _JsonFolder;

	private void Awake()
	{
		if (_GameManagerInstance == null) GameManagerInitialize();

		// GameManager 중복 방지
		else if (_GameManagerInstance != this)
		{
			Destroy(gameObject);
			return;
		}
		_JsonFolder = $"{Application.dataPath}/Resources/Json/BestScoreData/";

		// 최고 점수 데이터를 로드합니다.
		LoadBestData();

		// 해당 컴포넌트를 갖는 게임 오브젝트가 씬이 교체되어도 사라지지 않도록 합니다.
		DontDestroyOnLoad(gameObject);
	}

	// 최고 점수 데이터 파일을 저장합니다.
	private void SaveBestData()
	{
		// 경로를 생성합니다.
		Directory.CreateDirectory(_JsonFolder);

		// Json 파일 저장을 위해 최고 점수 데이터를 문자열로 변환합니다.
		string bestDataToString = JsonUtility.ToJson(bestData);
		/// - JsonUtility : Json 데이터 작업을 위한 정적 메서드를 제공하는 클래스입니다.

		// _JsonFolder 경로에 "BestScore.json" 파일을 생성하며, 그 파일에 
		// bestDataToString 문자열을 입력하여 파일을 생성합니다.
		System.IO.File.WriteAllText(_JsonFolder + "BestScore.json", bestDataToString);
	}

	// 최고 점수를 저장할 파일을 읽습니다.
	private void LoadBestData()
	{
		string readText = null;

		try
		{
			// 파일의 내용을 읽습니다.
			readText = File.ReadAllText(_JsonFolder + "BestScore.json");
		}
		// 경로를 찾지 못했을 때
		catch(DirectoryNotFoundException)
		{
			bestData = new BestDataInfo(DateTime.Now, 0.0, true);
		}
		// 파일을 찾지 못했을 때
		catch(FileNotFoundException)
		{
			bestData = new BestDataInfo(DateTime.Now, 0.0, true);
		}
		

		// 읽은 문자열을 BestDataInfo 형식으로 변환하여 최고 점수 데이터로 사용합니다.
		bestData = (readText == null) ?
			new BestDataInfo(DateTime.Now, 0.0, true) :
			JsonUtility.FromJson<BestDataInfo>(readText);
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

	// 점수를 증가시킵니다.
	public void AddScore(double addScore)
	{
		if (isGameOver) return;

		currentScore += addScore;

		// 점수가 음수가 될수 없도록 합니다.
		currentScore = (currentScore < 0.0) ? 0.0 : currentScore;
	}

	// 점수를 초기화합니다.
	public void ClearScore()
	{
		currentScore = 0.0;
	}

	// 최고 점수 갱신을 시도합니다.
	public void TryUpdateBestScore()
	{
		// 새로운 기록을 세우는 데 성공했는지를 나타낼 변수
		bool newRecord = false;

		// 최고 점수 기록이 존재하지 않다면
		if (bestData.empty)
			newRecord = true;

		// 최고 점수 기록이 존재하며
		// 최고 점수 기록에 실패하지 않았다면
		else if (bestData.bestScore < currentScore)
			newRecord = true;

		if (newRecord)
		{
			// 새로운 데이터를 생성합니다.
			bestData = new BestDataInfo(DateTime.Now, currentScore, false);

			// 파일을 저장합니다.
			SaveBestData();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehaviour : 해당 클래스를 상속받는 클래스는, 오브젝트에 추가할 수 있는
// 컴포넌트가 됩니다.
public class PlayerCharacter : MonoBehaviour
{
    // 플레이어 캐릭터를 이동시킬 때 적용될 속력입니다.
    [Header("플레이어 캐릭터 이동 속력")]
    /// - Header 애트리뷰트 : 인스펙터에 다음에 오는 문자열을 해당 속성 상단에 노출시킵니다.
    public float m_MoveSpeed = 10.0f;
    /// - 필드를 public 으로 선언할 경우 인스펙터에 노출됩니다.

    // 왼쪽 오른쪽 끝 X 좌표
    public const float MaxLeftPositionX = -7.395099f;
    public const float MaxRightPositionX = 7.002358f;

    // 배고픔 상태를 나타냅니다.
    public float hungry { get; private set; } = 50.0f;

    // 캐릭터 사망을 나타냅니다.
    public bool isDie { get; private set; }


    /*
    // 해당 컴포넌트가 생성된 후 처음으로 호출되는 메서드
    private void Awake() 
    {
        Debug.Log("Awake Method Call");
        // Console Window 에 로그를 출력합니다.

        // 오브젝트의 좌표를 지정한 위치로 변경합니다.
        //gameObject.transform.position = new Vector3(10.0f, 10.0f, 0.0f);
    } 

    // Awake 이후 오브젝트가 활성화 되었을 경우 호출되는 메서드
    private void OnEnable() { }

    // OnEnable 이후 오브젝트 내 컴포넌트가 실행을 시작할 때 호출되는 메서드
    private void Start() { }

    // 매 틱마다 호출되는 메서드
    /// - 고정 프레임
    private void FixedUpdate() { }

    /// - 매 프레임마다 호출
    private void Update() 
    {
        // 월드 기준 오른쪽 방향으로 이동시킵니다.
        //transform.Translate(Vector2.right * Time.deltaTime, Space.World);
        // - Translate(translation, relativeTo)
        /// - relativeTo 기준으로 translation 만큼씩 이동합니다.
        /// - relativeTo : Space.Self  -> 기준을 자기 자신으로 설정합니다.
        /// -            : Space.World -> 기준을 월드로 설정합니다.
        // - Time.deltaTime
        /// - 이전 Frame 에서 현재 Frame 까지 걸린 시간 간격에 대한 읽기 전용 프로퍼티


    }
    private void LateUpdate() { }

    // 오브젝트가 비활성화 되었을 경우 호출되는 메서드
    private void OnDisable() { }

    // 오브젝트가 소멸될 때 호출되는 메서드
    private void OnDestroy() { }

    // 게임 플레이가 종료되었을 경우 호출되는 메서드
    private void OnApplicationQuit() { }
    */

    // 이동할 방향을 나타냅니다.
    private Vector2 _Direction;

    private void Awake()
    {
        // 플레이어 캐릭터 설정
        GameManager.gameManager.SetPlayerCharacter(this);
    }

    private void Update()
    {
        // 키 입력을 받습니다.
        InputKey();

        // 캐릭터 이동
        MovePlayerCharacter();
    }

    // 키 입력을 받습니다.
    private void InputKey()
    {
        // 유니티에서의 키 입력
        /// - bool Input.GetKey(KeyCode key)
        ///   - 지정된 key 가 눌려있을 경우 참을 반환
        /// - bool Input.GetKeyDown(KeyCode key)
        ///   - 지정된 key 가 눌릴 때 한 번 참을 반환
        /// - bool Input.GetKeyUp(KeyCode key)
        ///   - 지정된 키가 눌렸다가 떼어졌을 경우 한 번 참을 반환
        ///   
        /// - float Input.GetAxis(string axisName)
        ///   -1.0f ~ 1.0f 사이의 값을 반환
        /// - float Input.GetAxisRaw(string axisName)
        ///   -1.0f, 0.0f, 1.0f 값을 반환

        //if (Input.GetKey(KeyCode.LeftArrow))
        //    transform.Translate(Vector2.left * Time.deltaTime, Space.World);
        //
        //else if (Input.GetKey(KeyCode.RightArrow))
        //    transform.Translate(Vector2.right * Time.deltaTime, Space.World);

        // 이동 방향 설정
        _Direction.x = Input.GetAxisRaw("Horizontal");
    }

    // 설정된 방향으로 이동시킵니다.
    private void MovePlayerCharacter()
    {
        transform.Translate(_Direction * m_MoveSpeed * Time.deltaTime, Space.World);

        // 캐릭터의 위치가 빙하 양 끝을 넘어가지 못하도록 합니다.
        Vector2 currentPosition = transform.position;

        //if (currentPosition.x < _MaxLeftPositionX)
        //    currentPosition.x = _MaxLeftPositionX;
        //else if (currentPosition.x > _MaxRightPositionX)
        //    currentPosition.x = _MaxRightPositionX;
        currentPosition.x = Mathf.Clamp(
            currentPosition.x, MaxLeftPositionX, MaxRightPositionX);
        /// Mathf.Clamp(value, min, max) : value 값을 min 과 max 사이의 값으로 변경하여
        /// 반환합니다.

        transform.position = currentPosition;
    }

    // 플레이어 캐릭터의 배고픔 값을 변경합니다.
    public void ChangeHungryValue(float value)
    {
        // 사망했다면 메서드 호출 종료
        if (isDie) return;

        hungry += value;

        if (hungry < 0.0f)
            isDie = true;

        hungry = Mathf.Clamp(hungry, 0.0f, 100.0f);

        Debug.Log("hungry = " + hungry);
    }

}

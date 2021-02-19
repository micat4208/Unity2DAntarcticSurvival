using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMovement : MonoBehaviour
{
	// Roll 회전을 사용하도록 할 것인지를 결정합니다.
	public bool useRollRotation { get; set; }

	// 떨어지는 속력
	private float _FallSpeed = 8;

	private void Update()
	{
		FallDown();
		AddRollRotation();
	}

	// 떨어짐 이동을 구현합니다.
	private void FallDown()
	{
		// 오브젝트의 위치가 -10 미만이라면
		if (transform.position.y < -10.0f)
		{
			// 오브젝트를 제거합니다.
			Destroy(gameObject);
			/// - Destroy (gameObject) : gameObject 를 제거합니다.
			/// - Destroy (gameObject, t) : gameObject 를 t 초 후에 제거합니다.
		}

		transform.Translate(Vector2.down * _FallSpeed * Time.deltaTime, Space.World);
	}

	// Roll 회전을 구현합니다.
	private void AddRollRotation()
	{
		// useRollRotation 가 false 라면 Roll 회전을 사용하지 않도록 합니다.
		if (!useRollRotation) return;

		// Z 축을 기준으로 회전시킵니다.
		/// 회전
		/// - 쿼터니언 회전 : transform.rotation (Quaternion 형식 사용)
		/// - 오일러 회전 : transform.eulerAngles (Vector3 형식 사용)
		transform.eulerAngles += new Vector3(0.0f, 0.0f, -90.0f * Time.deltaTime);
	}

}

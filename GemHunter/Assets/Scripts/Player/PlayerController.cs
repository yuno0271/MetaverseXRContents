using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private	MovementRigidbody2D	movement2D;
	private	PlayerRenderer		playerRenderer;

	private void Awake()
	{
		movement2D		= GetComponent<MovementRigidbody2D>();
		playerRenderer	= GetComponentInChildren<PlayerRenderer>();
	}

	private void Update()
	{
		float x = Input.GetAxisRaw("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		// 플레이어 이동 여부 검사
		bool isMoved = x != 0 || y != 0;
		// 플레이어 좌/우 반전
		if ( x != 0 ) playerRenderer.SpriteFlipX(x);
		// 플레이어 애니메이션 재생
		playerRenderer.OnMovement(isMoved ? 1 : 0);
		// 먼지 이펙트 재생
		playerRenderer.OnFootStepEffect(isMoved);
		// 플레이어 이동
		movement2D.MoveTo(new Vector3(x, y, 0));
	}
}


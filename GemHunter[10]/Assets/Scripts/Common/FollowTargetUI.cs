using UnityEngine;

public class FollowTargetUI : MonoBehaviour
{
	[SerializeField]
	private	Transform		target;
	private	RectTransform	rectTransform;
	private	Camera			mainCamera;

	private void Awake()
	{
		rectTransform	= GetComponent<RectTransform>();
		mainCamera		= Camera.main;
	}

	public void Setup(Transform target)
	{
		this.target = target;
	}

	private void FixedUpdate()
	{
		if ( target == null )
		{
			Destroy(gameObject);
			return;
		}

		rectTransform.position = mainCamera.WorldToScreenPoint(target.position);
	}
}


/*
 * Memo
 * 플레이어와 같이 Hierarchy View에 미리 배치해둔 오브젝트는 Inspector View에서 target을 설정하고,
 * 적과 같이 코드를 이용해 게임 도중 생성하는 오브젝트는 Setup() 메소드를 이용해 target을 설정한다.
 */
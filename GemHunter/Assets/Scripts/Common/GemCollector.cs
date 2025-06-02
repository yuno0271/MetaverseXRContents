using UnityEngine;
using UnityEngine.Events;

public class GemCollector : MonoBehaviour
{
	[SerializeField]
	private	GameObject		gemEffectPrefab;
	[SerializeField]
	private	RectTransform	uiElement;			// GEM이 이동할 목표 위치 (Gem Icon UI)
	[SerializeField]
	private	UnityEvent		onGemCollectEvent;	// GEM을 획득할 때 호출할 메소드 등록
	private	MemoryPool		memoryPool;         // GEM을 생성하고 관리할 메모리 풀

	private void Awake()
	{
		memoryPool	= new MemoryPool(gemEffectPrefab);
	}

	public void SpawnGemEffect(Vector2 point, int count=5)
	{
		for ( int i = 0; i < count; ++ i )
		{
			GameObject gem = memoryPool.ActivatePoolItem(point);
			gem.GetComponent<GemCollectEffect>().Setup(this, uiElement);
		}
	}

	public void OnGemCollect(GameObject gem)
	{
		onGemCollectEvent?.Invoke();		// GEM을 획득할 때 onGemCollectEvent에 등록된 메소드 호출
		memoryPool.DeactivatePoolItem(gem);	// 매개변수로 받아온 gem 오브젝트 비활성화
	}
}


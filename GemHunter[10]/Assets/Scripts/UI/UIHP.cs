using UnityEngine;
using UnityEngine.UI;

public class UIHP : MonoBehaviour
{
	[SerializeField]
	private	Image		image;
	[SerializeField]
	private	EntityBase	entity;

	private void Awake()
	{
		if ( entity != null ) entity.Stats.CurrentHP.OnValueChanged += UpdateHP;
	}

	public void Setup(EntityBase entity)
	{
		this.entity = entity;
		this.entity.Stats.CurrentHP.OnValueChanged += UpdateHP;
	}

	private void UpdateHP(Stat stat, float prev, float current)
	{
		image.fillAmount = entity.Stats.CurrentHP.Value / entity.Stats.GetStat(StatType.HP).Value;
	}
}


/*
 * Memo
 * 플레이어와 같이 Hierarchy View에 미리 배치해둔 오브젝트는 Inspector View에서 entity를 설정하고,
 * Awake() 메소드에서 이벤트 메소드 UpdateHP를 등록한다.
 * 
 * 적과 같이 코드를 이용해 게임 도중 생성하는 오브젝트는 Setup() 메소드를 이용해 entity를 설정하고,
 * 이벤트 메소드 UpdateHP를 등록한다.
 */
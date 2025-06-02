using UnityEngine;

public abstract class EntityBase : MonoBehaviour
{
	[SerializeField]
	private	EntityStats	stats;
	[SerializeField]
	private	Transform	middlePoint;

	public	EntityStats	Stats => stats;
	public	bool		IsDead => Stats.CurrentHP != null && Mathf.Approximately(Stats.CurrentHP.DefaultValue, 0f);
	public	Vector3		MiddlePoint => middlePoint != null ? middlePoint.position : Vector3.zero;
	public	EntityBase	Target	{ get; set; }

	protected virtual void Setup()
	{
		Stats.CurrentHP.DefaultValue = Stats.GetStat(StatType.HP).Value;
	}

	public void TakeDamage(float damage)
	{
		if ( IsDead ) return;

		// Evasion 스탯 확률로 공격 회피
		if ( Random.value < Stats.GetStat(StatType.Evasion).Value )	return;

		Stats.CurrentHP.DefaultValue -= damage;

		if ( Mathf.Approximately(Stats.CurrentHP.DefaultValue, 0f) )
		{
			OnDie();	// Entity 사망 처리
		}
	}

	protected abstract void OnDie();
}


/* // middlePoint 변수에 값을 할당하지 않았을 때 예외처리
		if ( middlePoint == null )
		{
			for ( int i = 0; i < transform.childCount; ++ i )
			{
				Transform child = transform.GetChild(i);
				if ( child.name.Equals("MiddlePoint") )
				{
					middlePoint = child;
				}
			}
		}
*/
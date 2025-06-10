﻿using UnityEngine;

public class ProjectileGlobal : ProjectileBase
{
	[SerializeField]
	private	Transform		hitEffect;		// 피격 이펙트
	[SerializeField]
	private	UIDamageText	damageText;		// 데미지 텍스트 UI

	protected SkillBase		skillBase;
	protected float			currentDuration;
	protected float			currentAttackRate = 0;
	protected float			damage;

	public override void Setup(SkillBase skillBase, float damage)
	{
		this.skillBase	= skillBase;
		this.damage		= damage;
		currentDuration	= Time.time;
	}

	/// <summary>
	/// 지속 시간(Duration)이 있는 스킬만 base.Process() 호출
	/// </summary>
	public override void Process()
	{
		// 지속 시간이 없는 스킬이 base.Process()를 실행했을 때 실행하지 않도록 예외 처리
		if ( skillBase.GetStat(StatType.Duration) == null ) return;

		// 발사체 생성 시점(currentDuration)부터 StatType.Duration 시간이 지나면 발사체 삭제
		if ( Time.time - currentDuration > skillBase.GetStat(StatType.Duration).Value )
		{
			Destroy(gameObject);
		}
	}

	protected void TakeDamage(EntityBase entity)
	{
		if ( hitEffect != null )
		{
			Instantiate(hitEffect, entity.MiddlePoint, Quaternion.identity);
		}
		if ( damageText != null )
		{
			UIDamageText clone = Instantiate(damageText, entity.MiddlePoint, Quaternion.identity);
			clone.Setup(damage.ToString("F0"), Color.white);
		}

		entity.TakeDamage(damage);
	}
}


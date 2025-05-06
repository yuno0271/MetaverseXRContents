using System.Linq;
using UnityEngine;

[System.Serializable]
public struct EntityStats
{
	[Header("Level, Exp")]
	public	int		level;				// 레벨
	public	float	exp;				// 경험치

	/*[Header("Attack")]
	public	float	damage;				// 공격력
	public	float	cooldownTime;		// 기본 공격 쿨타임
	public	float	critialChance;		// 크리티컬 확률
	public	float	criticalMultiplier;	// 크리티컬 공격력

	[Header("Defense")]
	public	float	currentHP;			// 현재 체력
	public	float	maxHP;				// 최대 체력
	public	float	evasion;			// 회피율*/

	[Header("Current Stats")]
	[SerializeField]
	private	Stat	currentHP;

	[Header("Stats")]
	[SerializeField]
	private	Stat[]	stats;

	public readonly Stat CurrentHP					=> currentHP;
	public readonly	Stat GetStat(Stat stat)			=> stats.FirstOrDefault(s => s.StatType == stat.StatType);
	public readonly Stat GetStat(StatType statType)	=> stats.FirstOrDefault(s => s.StatType == statType);
}


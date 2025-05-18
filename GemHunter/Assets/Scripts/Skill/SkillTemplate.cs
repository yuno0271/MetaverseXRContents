using System.Collections.Generic;
using UnityEngine;

public enum SkillType	 { Buff = 0, Emission, Sustained, Global, }
public enum SkillElement { None = -1, Ice = 100, Fire, Wind, Light, Dark, Count = 5 }

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillAsset", order = 0)]
public class SkillTemplate : ScriptableObject
{
	[Header("공통")]
	public	string			skillName;			// 스킬 이름
	public	SkillType		skillType;			// 스킬 타입
	public	SkillElement	element;			// 스킬 속성
	public	int				maxLevel;			// 스킬 최대 레벨
	[TextArea(1, 30)]
	public	string			description;		// 스킬 설명
	public	Sprite			disableIcon;		// 스킬 미습득 아이콘
	public	Sprite			enableIcon;			// 스킬 습득 아이콘

	[Header("버프 스킬")]
	public	List<Stat>		buffStatList;		// 버프 스킬

	[Header("공격 스킬")]
	public	List<Stat>		attackBaseStats;	// 공격 스킬 스탯 정보
	public	List<Stat>		attackBuffStats;	// 레벨업 시 추가 스탯
	public	GameObject		projectile;			// 발사체 프리팹
}


/*
public class SkillTemplate : ScriptableObject
{
	...

	[Header("버프 스킬")]
	public	BuffStats		buffStats;
}

[System.Serializable]
public struct BuffStats
{
	public	float	criticalChance;			// 크리티컬 확률
	public	float	criticalMultiplier;		// 크리티컬 계수
	public	float	damage;					// 공격력 계수
	public	float	cooldownTime;			// 쿨타임
	public	float	metastasisCount;		// 발사체 전이 횟수
	public	float	evasion;				// 회피율
	public	float	hpRecovery;				// 초당 체력 회복량
	public	float	projectileCount;		// 추가 발사체 개수
}

[System.Serializable]
public struct ElementalBonus
{
	// 각 속성 추가 공격력(50% -> 0.5)
	public	float	ice;
	public	float	fire;
	public	float	wind;
	public	float	light;
	public	float	dark;

	// 각 속성 추가 효과 (빙결, 관통 등)
	public	float	freezing;
	public	float	splash;
	public	float	penetrated;
	public	float	hp;
	public	float	dot;
}
*/

﻿using UnityEngine;

public class EnemyBase : EntityBase
{
	[SerializeField]
	private	Transform		hudPoint;
	[SerializeField]
	private	GameObject		uiPrefab;
	[SerializeField]
	private	int				gemMin = 5, gemMax = 21;

	private	EnemySpawner	enemySpawner;
	private	GemCollector	gemCollector;

	private void Awake()
	{
		Setup();
	}

	protected override void Setup()
	{
		// 기본 체력은 DefaultValue에 할당하기 때문에 추가 체력(BonusValue)만 설정
		Stats.GetStat(StatType.HP).BonusValue = 50 * (Stats.GetStat(StatType.Level).Value - 1);

		base.Setup();
	}

	public void Initialize(EnemySpawner enemySpawner, Transform parent, GemCollector gemCollector)
	{
		this.enemySpawner = enemySpawner;
		this.gemCollector = gemCollector;

		GameObject clone = Instantiate(uiPrefab, parent);
		clone.transform.localScale = Vector3.one;
		clone.GetComponent<FollowTargetUI>().Setup(hudPoint);
		clone.GetComponentInChildren<UIHP>().Setup(this);
	}

	protected override  void OnDie()
	{
		// 임의의 개수(gemMin ~ gemMax-1)만큼 GEM을 생성합니다.
		gemCollector.SpawnGemEffect(transform.position, Random.Range(gemMin, gemMax));
		// 적은 레벨업을 하지 않기 때문에 적의 경험치 스탯만큼 플레이어 경험치 증가
		(Target as PlayerBase).AccumulationExp += Stats.CurrentExp.Value;
		// 적 본인(this) 사망처리
		enemySpawner.Deactivate(this);
	}
}


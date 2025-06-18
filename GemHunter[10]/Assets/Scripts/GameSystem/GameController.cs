using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private	ChapterData[]	chapters;
	[SerializeField]
	private	EnemySpawner	enemySpawner;
	[SerializeField]
	private	TextMeshPro		textStageNumber;
	[SerializeField]
	private	UIRewardResult	uiRewardResult;
	[SerializeField]
	private	PlayerBase		player;
	[SerializeField]
	private	float			enemyCountScale = 0.15f;

	private	int				currentChapter;
	private	int				maxStage;
	private	int				currentStage = 0;
	private	int				baseEnemyCount = 10;

	private void Start()
	{
		currentChapter	= PlayerPrefs.GetInt(Constants.ChapterIndex);
		maxStage		= chapters[currentChapter].StageDataTable.maxStage;

		// 현재 스테이지에 남아있는 적 숫자가 0이면 다음 스테이지로 넘어가도록 설정
		EnemySpawner.exitEvent.AddListener(SetupStage);
		// 스테이지 설정, 스테이지에 등장하는 적 생성
		SetupStage();
	}

	public void SetupStage()
	{
		currentStage ++;

		if ( currentStage > maxStage )
		{
			GameClear();
			return;
		}

		// 맵에 출력하는 currentStage Text UI 갱신
		textStageNumber.text = $"STAGE {currentStage:D2}";
		// 스테이지에 따라 등장하는 적 숫자 연산/생성
		enemySpawner.SpawnEnemys((int)(baseEnemyCount + currentStage * enemyCountScale));
	}

	public void SetTimeScale(float scale)
	{
		Time.timeScale = scale;
	}

	public void GameClear()
	{
		SetTimeScale(0);
		
		long baseExp	 = (long)(currentStage * (5 + (currentChapter+1) * 1.2f));
		long bonusExp	 = (long)Mathf.Pow(2, (currentChapter+1)) * 100;
		long bonusGem	 = (currentChapter+1) * 5000;
		bool isNewRecord = Database.DBItem.chapters[currentChapter].bestStage != maxStage;

		// Database 클래스의 DBItem에 데이터 저장
		Database.DBItem.player.experience += (baseExp + bonusExp);
		Database.DBItem.goods.gem += (player.GEM + bonusGem);
		Database.DBItem.chapters[currentChapter].bestStage = maxStage;

		if ( currentChapter+1 < Database.DBItem.chapters.Length )
			Database.DBItem.chapters[currentChapter+1].isUnlock = true;
		
		Database.Write();		// DBItem에 저장되어 있는 데이터를 파일에 저장

		// 새 기록 여부, 클리어 여부, 챕터, 스테이지, 보상 정보 전달 (보상은 원하는 개수만큼 추가 가능)
		uiRewardResult.OnRewardResult(isNewRecord, true, currentChapter, maxStage,
			new (RewardType, long)[] { (RewardType.GEM, player.GEM), (RewardType.EXP, baseExp),
									   (RewardType.GEM, bonusGem), (RewardType.EXP, bonusExp) });
	}

	public void GameOver()
	{
		SetTimeScale(0);

		long exp		 = (long)(currentStage * (5 + (currentChapter+1) * 1.2f));
		bool isNewRecord = Database.DBItem.chapters[currentChapter].bestStage < currentStage;
		
		// Database 클래스의 DBItem에 데이터 저장
		Database.DBItem.player.experience += exp;
		Database.DBItem.goods.gem += player.GEM;
		if ( isNewRecord ) Database.DBItem.chapters[currentChapter].bestStage = currentStage;

		Database.Write();		// DBItem에 저장되어 있는 데이터를 파일에 저장

		// 새 기록 여부, 챕터, 스테이지, 보상 정보 전달 (보상은 원하는 개수만큼 추가 가능)
		uiRewardResult.OnRewardResult(isNewRecord, false, currentChapter, currentStage,
			new (RewardType, long)[] { (RewardType.GEM, player.GEM), (RewardType.EXP, exp) });
	}
}


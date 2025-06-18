using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILobbyTopBar : MonoBehaviour
{
	[SerializeField]
	private	LevelData		levelData;
	[SerializeField]
	private	TextMeshProUGUI	textLevel;
	[SerializeField]
	private	Slider			fillGaugeEXP;
	[SerializeField]
	private	TextMeshProUGUI	textHeart;
	[SerializeField]
	private TextMeshProUGUI textGEMCount;

	private void Awake()
	{
		// 현재는 로비에서 재화를 사용하지 않기 때문에 Lobby 씬을 로드할 때 1회만 호출
		// 일반적으로는 Stat.cs과 같이 재화에 delegate, event를 설정하고,
		// 값이 변경될 때마다 호출하도록 설정해서 사용
		
		// 레벨, 경험치 계산 및 출력
		UpdateLevel();
		// 보유 GEM 출력
		textGEMCount.text = NotateNumber.Transform((long)Database.DBItem.goods.gem);
	}

	private void UpdateLevel()
	{
		int level = Database.DBItem.player.level;

		// 경험치가 최대이면 레벨업
		while ( Database.DBItem.player.experience >= levelData.MaxExperience[level-1] )
		{
			if ( level > levelData.MaxExperience.Length ) level = levelData.MaxExperience.Length;

			Database.DBItem.player.experience -= levelData.MaxExperience[level-1];
			Database.DBItem.player.level ++;
		}
		Database.Write();

		fillGaugeEXP.value = Database.DBItem.player.experience / levelData.MaxExperience[level-1];
		textLevel.text = Database.DBItem.player.level.ToString();
	}
}


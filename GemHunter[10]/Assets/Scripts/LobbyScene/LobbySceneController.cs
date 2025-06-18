using UnityEngine;

public class LobbySceneController : MonoBehaviour
{
	[SerializeField]
	private	GameObject		chapterIconPrefab;
	[SerializeField]
	private	Transform		parentContent;
	[SerializeField]
	private	ChapterData[]	allChapter;
	[SerializeField]
	private	SwipeUI			swipeUI;

	private void Awake()
	{
		for ( int i = 0; i < allChapter.Length; ++ i )
		{
			GameObject icon = Instantiate(chapterIconPrefab, parentContent);
			icon.GetComponent<UIChapterIcon>().Setup(i, allChapter[i]);
		}
	}

	public void ButtonEvent_GameStart()
	{
		int index = swipeUI.CurrentPage;
	
		if ( Database.DBItem.chapters[index].isUnlock == false)
		{
			Logger.Log("잠겨있는 챕터입니다.");
			return;
		}

		PlayerPrefs.SetInt(Constants.ChapterIndex, index);

		SceneLoader.Instance.LoadScene(SceneNames.Game);
	}
}


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum SceneNames { Intro = 0, Lobby, Game }

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader Instance { get; private set; }

	[SerializeField]
	private	GameObject		loadingScreen;		// 로딩 화면
	[SerializeField]
	private	Image			loadingBackground;	// 로딩 화면에 출력되는 배경 이미지
	[SerializeField]
	private	Sprite[]		loadingSprites;		// 배경 이미지 목록
	[SerializeField]
	private	Slider			loadingProgress;	// 로딩 진행도
	[SerializeField]
	private	TextMeshProUGUI	textProgress;		// 로딩 진행도 텍스트

	private void Awake()
	{
		if ( Instance != null && Instance != this )
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void LoadScene(string name)
	{
		int index = Random.Range(0, loadingSprites.Length);
		loadingBackground.sprite = loadingSprites[index];
		loadingProgress.value	 = 0f;
		loadingScreen.SetActive(true);

		StartCoroutine(LoadSceneAsync(name));
	}

	public void LoadScene(SceneNames name)
	{
		LoadScene(name.ToString());
	}

	private IEnumerator LoadSceneAsync(string name)
	{
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);

		// 비동기 작업(씬 불러오기)이 완료될 때까지 반복
		while ( asyncOperation.isDone == false )
		{
			// 비동기 작업의 진행 상황 (0.0 ~ 1.0)
			loadingProgress.value = asyncOperation.progress;
			textProgress.text = $"{Mathf.RoundToInt(asyncOperation.progress * 100)}%";

			yield return null;
		}
		
		float changeDelay = 0.5f;
		yield return new WaitForSeconds(changeDelay);

		loadingScreen.SetActive(false);
	}
}


/*	로딩 여부에 관계없이 원하는 시간만큼 대기
	private IEnumerator LoadSceneAsync(string name)
	{
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);

		float time = 0f;
		float loadingTime = 2.5f;
		while ( time < 1f )
		{
			time += Time.deltaTime / loadingTime;
			loadingProgress.value = time;
			textProgress.text = $"{Mathf.RoundToInt(time * 100)}%";

			yield return null;
		}
		
		float changeDelay = 0.5f;
		yield return new WaitForSeconds(changeDelay);

		loadingScreen.SetActive(false);
	}
*/
// 씬이 준비되었을 때 즉시 활성화 할지 선택 (default : true)
//asyncOperation.allowSceneActivation = false;
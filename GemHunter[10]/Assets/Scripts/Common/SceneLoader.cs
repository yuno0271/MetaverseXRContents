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
	private	GameObject		loadingScreen;		// �ε� ȭ��
	[SerializeField]
	private	Image			loadingBackground;	// �ε� ȭ�鿡 ��µǴ� ��� �̹���
	[SerializeField]
	private	Sprite[]		loadingSprites;		// ��� �̹��� ���
	[SerializeField]
	private	Slider			loadingProgress;	// �ε� ���൵
	[SerializeField]
	private	TextMeshProUGUI	textProgress;		// �ε� ���൵ �ؽ�Ʈ

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

		// �񵿱� �۾�(�� �ҷ�����)�� �Ϸ�� ������ �ݺ�
		while ( asyncOperation.isDone == false )
		{
			// �񵿱� �۾��� ���� ��Ȳ (0.0 ~ 1.0)
			loadingProgress.value = asyncOperation.progress;
			textProgress.text = $"{Mathf.RoundToInt(asyncOperation.progress * 100)}%";

			yield return null;
		}
		
		float changeDelay = 0.5f;
		yield return new WaitForSeconds(changeDelay);

		loadingScreen.SetActive(false);
	}
}


/*	�ε� ���ο� ������� ���ϴ� �ð���ŭ ���
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
// ���� �غ�Ǿ��� �� ��� Ȱ��ȭ ���� ���� (default : true)
//asyncOperation.allowSceneActivation = false;
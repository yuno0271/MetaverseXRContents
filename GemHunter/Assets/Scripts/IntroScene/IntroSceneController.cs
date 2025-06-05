using System.Collections;
using UnityEngine;
using TMPro;

public class IntroSceneController : MonoBehaviour
{
	[SerializeField]
	private	SceneNames		nextScene;
	[SerializeField]
	private	TextMeshProUGUI	textPressAnyKey;

	private IEnumerator Start()
	{
		while ( true )
		{
			yield return StartCoroutine(FadeEffect.Fade(textPressAnyKey, 1, 0));

			yield return StartCoroutine(FadeEffect.Fade(textPressAnyKey, 0, 1));
		}
	}

	private void Update()
	{
		if ( Input.anyKeyDown )
		{
			SceneLoader.Instance.LoadScene(nextScene);
		}
	}
}


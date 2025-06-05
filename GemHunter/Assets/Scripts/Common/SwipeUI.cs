using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
	[SerializeField]
	private	Scrollbar	scrollBar;					// Scrollbar�� ��ġ�� �������� ���� ������ �˻�
	[SerializeField]
	private	float		swipeTime = 0.2f;			// �������� Swipe �Ǵ� �ð�
	[SerializeField]
	private	float		swipeDistance = 50.0f;		// �������� Swipe�Ǳ� ���� �������� �ϴ� �ּ� �Ÿ�

	private	float[]		scrollPageValues;			// �� �������� ��ġ �� [0.0 - 1.0]
	private	float		valueDistance = 0;			// �� ������ ������ �Ÿ�
	private	int			currentPage = 0;			// ���� ������
	private	int			maxPage = 0;				// �ִ� ������
	private	float		startTouchX;				// ��ġ ���� ��ġ
	private	float		endTouchX;					// ��ġ ���� ��ġ
	private	bool		isSwipeMode = false;		// ���� Swipe�� �ǰ� �ִ��� üũ

	// ���� ������ Index ����
	public	int			CurrentPage => currentPage;

	private void Start()
	{
		// �ִ� �������� ��
		maxPage = transform.childCount;
		// ��ũ�� �Ǵ� �������� �� value ���� �����ϴ� �迭 �޸� �Ҵ�
		scrollPageValues = new float[transform.childCount];
		// ��ũ�� �Ǵ� ������ ������ �Ÿ�
		valueDistance = 1f / (scrollPageValues.Length - 1f);

		// ��ũ�� �Ǵ� �������� �� value ��ġ ���� [0 <= value <= 1]
		for (int i = 0; i < scrollPageValues.Length; ++ i )
		{
			scrollPageValues[i] = valueDistance * i;
		}

		// ���� ������ �� 0�� �������� �� �� �ֵ��� ����
		SetScrollBarValue(0);
	}

	private void Update()
	{
		UpdateInput();
	}

	public void SetScrollBarValue(int index)
	{
		currentPage		= index;
		scrollBar.value	= scrollPageValues[index];
	}

	private void UpdateInput()
	{
		// ���� Swipe�� �������̸� ��ġ �Ұ�
		if ( isSwipeMode == true ) return;

		#if UNITY_EDITOR
		if ( Input.GetMouseButtonDown(0) )
		{
			startTouchX = Input.mousePosition.x;	// ��ġ ���� ���� (Swipe ���� ����)
		}
		else if ( Input.GetMouseButtonUp(0) )
		{
			endTouchX = Input.mousePosition.x;		// ��ġ ���� ���� (Swipe ���� ����)
			UpdateSwipe();
		}
		#endif

		#if UNITY_ANDROID
		if ( Input.touchCount == 1 )
		{
			Touch touch = Input.GetTouch(0);

			if ( touch.phase == TouchPhase.Began )
			{
				startTouchX = touch.position.x;		// ��ġ ���� ���� (Swipe ���� ����)
			}
			else if ( touch.phase == TouchPhase.Ended )
			{
				endTouchX = touch.position.x;		// ��ġ ���� ���� (Swipe ���� ����)
				UpdateSwipe();
			}
		}
		#endif
	}

	private void UpdateSwipe()
	{
		// �ʹ� ���� �Ÿ��� �������� ���� Swipe X
		if ( Mathf.Abs(startTouchX-endTouchX) < swipeDistance )
		{
			// ���� �������� Swipe�ؼ� ���ư���
			StartCoroutine(OnSwipeOneStep(currentPage));
			return;
		}

		// Swipe ����
		bool isLeft = startTouchX < endTouchX ? true : false;

		if ( isLeft == true )							// �̵� ������ ������ ��
		{
			if ( currentPage == 0 ) return;				// ���� �������� ���� ���̸� ����

			currentPage --;								// �������� �̵��� ���� ���� �������� 1 ����
		}
		else											// �̵� ������ �������� ��
		{
			if ( currentPage == maxPage - 1 ) return;	// ���� �������� ������ ���̸� ����

			currentPage ++;								// ���������� �̵��� ���� ���� �������� 1 ����
		}

		// currentIndex��° �������� Swipe�ؼ� �̵�
		StartCoroutine(OnSwipeOneStep(currentPage));
	}

	/// <summary>
	/// �������� �� �� ������ �ѱ�� Swipe ȿ�� ���
	/// </summary>
	private IEnumerator OnSwipeOneStep(int index)
	{
		float start		= scrollBar.value;
		float percent	= 0;

		isSwipeMode = true;

		while ( percent < 1 )
		{
			percent += Time.deltaTime / swipeTime;

			scrollBar.value = Mathf.Lerp(start, scrollPageValues[index], percent);

			yield return null;
		}

		isSwipeMode = false;
	}
}


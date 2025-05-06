using System.Collections;
using UnityEngine;

public enum EnemyState { None = -1, Attack, }

public class EnemyFSM : MonoBehaviour
{
	[SerializeField]
	private	GameObject	projectilePrefab;
	[SerializeField]
	private	Transform	projectileSpawnPoint;
	
	private	EnemyBase	owner;
	private	EnemyState	enemyState;

	private void Awake()
	{
		owner = GetComponent<EnemyBase>();

		ChangeState(EnemyState.Attack);
	}

	public void Setup(EntityBase target)
	{
		owner.Target = target;
	}

	public void ChangeState(EnemyState newState)
	{
		// Tip. 열거형 변수.ToString()을 하게 되면 열거형에 정의된 변수 이름을 string으로 받아온다
		// ex) enemyState가 현재 EnemyState.Idle이면 "Idle" 문자열
		// 이를 이용해 열거형의 이름과 코루틴 이름을 일치시켜
		// 열거형 변수에 따라 코루틴 함수 재생을 제어할 수 있다.

		// 이전에 재생중이던 상태 종료
		StopCoroutine(enemyState.ToString());
		// 상태 변경
		enemyState = newState;
		// 새로운 상태 재생
		StartCoroutine(enemyState.ToString());
	}

	private IEnumerator Attack()
	{
		var wait = new WaitForSeconds(owner.Stats.GetStat(StatType.CooldownTime).Value);

		while ( true )
		{
			yield return wait;

			Vector3 target = owner.Target.MiddlePoint;
			GameObject clone = Instantiate(projectilePrefab);
			clone.transform.position = projectileSpawnPoint.position;
			clone.GetComponent<EnemyProjectile>().Setup(target, owner.Stats.GetStat(StatType.Damage).Value);
		}
	}
}


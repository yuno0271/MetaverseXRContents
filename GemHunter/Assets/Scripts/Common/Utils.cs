using UnityEngine;

public static class Utils
{
	/// <summary>
	/// 회전 값이 Vector3.zero일 때 -> 방향을 보고 있는 오브젝트 기준
	/// 방향이 다르면 weight에 각도를 더해준다.
	/// 반시계 방향 기준 +, 시계 방향 기준 -
	/// </summary>
	public static Quaternion RotateToTarget(Vector2 owner, Vector2 target, float weight=0)
	{
		// 원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구하는 극 좌표계 이용
		// 각도 = arctan(y/x)
		// x, y 변위값 구하기
		float dx = target.x - owner.x;
		float dy = target.y - owner.y;

		// x, y 변위값을 바탕으로 각도 구하기
		// 각도가 radian 단위이기 때문에 Mathf.Rad2Deg를 곱해 도 단위를 구함
		float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

		return Quaternion.Euler(0, 0, degree-weight);
	}
}


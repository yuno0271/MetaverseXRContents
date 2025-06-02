using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	private	Tilemap			tilemap;
	[SerializeField]
	private	GameObject[]	enemyPrefabs;
	[SerializeField]
	private	Transform		parentTransform;
	[SerializeField]
	private	GemCollector	gemCollector;
	[SerializeField]
	private	EntityBase		target;
	[SerializeField]
	private	int				enemyCount = 10;

	private	Vector3			offset = new Vector3(0.5f, 0.5f, 0);
	private	List<Vector3>	possibleTiles = new List<Vector3>();

	public static List<EntityBase>	Enemies { get; private set; } = new List<EntityBase>();

	private void Awake()
	{
		// Tilemap의 Bounds 재설정 (맵을 수정했을 때 Bounds가 변경되지 않는 문제 해결)
		tilemap.CompressBounds();
		// 타일맵의 모든 타일을 대상으로 적 배치가 가능한 타일 계산
		CalculatePossibleTiles();

		// 임의의 타일에 10명의 적 생성
		for ( int i = 0; i < enemyCount; ++ i )
		{
			int type	= Random.Range(0, enemyPrefabs.Length);
			int index	= Random.Range(0, possibleTiles.Count);

			GameObject clone = Instantiate(enemyPrefabs[type], possibleTiles[index], Quaternion.identity, transform);
			clone.GetComponent<EnemyBase>().Initialize(this, parentTransform, gemCollector);
			clone.GetComponent<EnemyFSM>().Setup(target);

			Enemies.Add(clone.GetComponent<EntityBase>());
		}
	}

	private void CalculatePossibleTiles()
	{
		BoundsInt	bounds	 = tilemap.cellBounds;
		TileBase[]	allTiles = tilemap.GetTilesBlock(bounds);

		// 외곽의 벽과 붙어있는 타일은 제외하기 위해
		// x, y의 시작 값은 1, 끝 값은 bounds.size.x - 1, bounds.size.y - 1
		for ( int y = 1; y < bounds.size.y - 1; ++ y )
		{
			for ( int x = 1; x < bounds.size.x - 1; ++ x )
			{
				TileBase tile = allTiles[y * bounds.size.x + x];

				if ( tile != null )
				{
					Vector3Int	localPosition	= bounds.position + new Vector3Int(x, y);
					Vector3		position		= tilemap.CellToWorld(localPosition) + offset;
					position.z = 0;

					possibleTiles.Add(position);
				}
			}
		}
	}

	public void Deactivate(EntityBase enemy)
	{
		Enemies.Remove(enemy);
		Destroy(enemy.gameObject);
	}
}


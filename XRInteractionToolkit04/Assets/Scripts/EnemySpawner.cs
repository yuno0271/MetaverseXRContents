using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private bool isPlayOnStart = true;  // ������ �������ڸ��� ���� ������ ������?
    [SerializeField]
    private float startFactor = 1.0f;      // �� ���� ���� �⺻ ��
    [SerializeField]
    private float additiveFactor = 0.1f;    // �� ���� ���ڿ� �� �� �������� ��
    [SerializeField]
    private float delayPerSpawnGroup = 3.0f;    // �� ���� �ֱ�

    private WaitForSeconds waitSpawnEnemyGroup;
    private WaitForSeconds waitSpawnEnemy;

    private void Awake()
    {
        waitSpawnEnemyGroup = new WaitForSeconds(delayPerSpawnGroup);
        waitSpawnEnemy = new WaitForSeconds(0.02f);

        if(isPlayOnStart )
        {
            Play();
        }
    }

    public void Play()
    {
        StartCoroutine(nameof(SpawnProcess));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnProcess()
    {
        float factor = startFactor;

        while(true)
        {
            yield return waitSpawnEnemyGroup;

            yield return StartCoroutine(SpawnEnemy(factor));

            factor += additiveFactor;
        }
    }

    private IEnumerator SpawnEnemy(float factor)
    {
        float spawnCount = Random.Range(factor, factor * 2);

        for(int i = 0; i < spawnCount;i++)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation, transform);

            if(Random.value < 0.2f)
            {
                yield return waitSpawnEnemy;
            }
        }
    }
}

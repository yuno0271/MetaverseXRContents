using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private bool isPlayOnStart = true;  // 게임을 시작하자마자 적을 생성할 것인지?
    [SerializeField]
    private float startFactor = 1.0f;      // 적 생성 숫자 기본 값
    [SerializeField]
    private float additiveFactor = 0.1f;    // 적 생성 숫자에 매 턴 더해지는 값
    [SerializeField]
    private float delayPerSpawnGroup = 3.0f;    // 적 생성 주기

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

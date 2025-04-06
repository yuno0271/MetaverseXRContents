using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onCreated;
    [SerializeField]
    private UnityEvent onDestroyed;

    [SerializeField]
    private float destroyDelay = 1.0f;
    private bool isDestroyed = false;

    private void Start()
    {
        if(onCreated != null)onCreated.Invoke();

        EnemyManager.Instance.OnSpawn(this);
    }

    public void Destroy()
    {
        if(isDestroyed) return;

        isDestroyed = true;

        //destroyParticle.Play();
        //destroyAudio.Play();

        Destroy(gameObject, destroyDelay);

        if(onDestroyed != null) onDestroyed.Invoke();

        EnemyManager.Instance.OnDestroyed(this);
    }
}

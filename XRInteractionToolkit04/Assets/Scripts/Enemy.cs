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
        
        // Debug. 테스트용 코드
        Invoke("Destroy", Random.Range(1.0f, 6.0f));

        if(onCreated != null)onCreated.Invoke();
    }

    public void Destroy()
    {
        if(isDestroyed) return;

        isDestroyed = true;

        //destroyParticle.Play();
        //destroyAudio.Play();

        Destroy(gameObject, destroyDelay);

        if(onDestroyed != null) onDestroyed.Invoke();
    }
}

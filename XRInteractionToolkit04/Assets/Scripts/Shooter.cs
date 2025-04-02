using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private LayerMask hittableMask;
    [SerializeField]
    private GameObject hitEffectPrefab;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private float shootDelay = 0.1f;
    [SerializeField]
    private float maxDistance = 100.0f;
    [SerializeField]
    private UnityEvent<Vector3> onShootSuccess;
    [SerializeField]
    private UnityEvent onShootFail;

    private Magazine magazine;
    private WaitForSeconds waitShootDelay;

    private void Awake()
    {
        magazine = GetComponent<Magazine>();
        waitShootDelay = new WaitForSeconds(shootDelay);

        Stop();
    }

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(Process));
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator Process()
    {
        while (true)
        {
            if(magazine.Use())
            {
                Shoot();
            }
            else
            {
                onShootFail?.Invoke();
            }

            yield return waitShootDelay;
        }
    }

    private void Shoot()
    {
        if(Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hitInfo, maxDistance, hittableMask))
        {
            Instantiate(hitEffectPrefab, hitInfo.point, Quaternion.identity);

            Hittable hitObject = hitInfo.transform.GetComponent<Hittable>();
            hitObject?.Hit();

            onShootSuccess?.Invoke(hitInfo.point);
        }
        else
        {
            Vector3 hitPoint = shootPoint.position + shootPoint.forward * maxDistance;
            onShootSuccess?.Invoke(hitPoint);
        }
    }
}

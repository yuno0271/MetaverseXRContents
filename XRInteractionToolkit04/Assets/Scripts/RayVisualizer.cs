using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField]
    private LineRenderer ray;
    [SerializeField]
    private LayerMask hitRayMask;
    [SerializeField]
    private float distance = 100.0f;

    [Header("Reticle Point")]
    [SerializeField]
    private GameObject reticlePoint;
    [SerializeField]
    private bool showReticle = true;

    private void Awake()
    {
        Off();
    }

    public void On()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(Process));
    }

    public void Off()
    {
        StopAllCoroutines();

        ray.enabled = false;
        reticlePoint.SetActive(false);
    }

    private IEnumerator Process()
    {
        while(true)
        {
            if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, distance, hitRayMask))
            {
                ray.SetPosition(1, transform.InverseTransformPoint(hitInfo.point));
                ray.enabled = true;

                reticlePoint.transform.position = hitInfo.point;
                reticlePoint.SetActive(showReticle);
            }
            else
            {
                ray.enabled = false;

                reticlePoint.SetActive(false);
            }

            yield return null;
        }
    }
}

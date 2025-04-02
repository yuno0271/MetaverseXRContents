using System.Collections;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.05f;
    private Light target;

    private void Awake()
    {
        target = GetComponent<Light>();
    }

    public void Call()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        target.enabled = true;

        yield return new WaitForSeconds(duration);

        target.enabled = false;
    }
}

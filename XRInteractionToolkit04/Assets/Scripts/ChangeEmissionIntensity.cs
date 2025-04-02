using UnityEngine;

public class ChangeEmissionIntensity : MonoBehaviour
{
    [SerializeField]
    private float min = 0.0f;
    [SerializeField]
    private float max = 3.0f;
    private Renderer target;

    private void Awake()
    {
        target = GetComponent<Renderer>();
    }

    public void Call(float ratio)
    {
        float intensity = Mathf.Lerp(min, max, ratio);
        target.material.SetColor("_EmissionColor", target.material.color * intensity);
    }
}

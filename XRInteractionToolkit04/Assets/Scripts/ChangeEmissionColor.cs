using UnityEngine;

public class ChangeEmissionColor : MonoBehaviour
{
    [SerializeField]
    private float intensity = 5.0f;
    private Renderer target;

    private void Awake()
    {
        target = GetComponent<Renderer>();
    }

    public void Call(Color color)
    {
        target.material.SetColor("_EmissionColor", color * intensity);
    }
}

using UnityEngine;
using UnityEngine.Events;

public class RandomColor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Color> onCreated;
    
    [SerializeField]
    private float hueMin = 0f;
    [SerializeField]
    private float hueMax = 1.0f;
    [SerializeField]
    private float saturationMin = 0.7f;
    [SerializeField]
    private float saturationMax = 1.0f;
    [SerializeField]
    private float valueMin = 0.7f;
    [SerializeField]
    private float valueMax = 1.0f;

    public void Call()
    {
        Color color = Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);

        if(onCreated != null) onCreated.Invoke(color);
    }
}

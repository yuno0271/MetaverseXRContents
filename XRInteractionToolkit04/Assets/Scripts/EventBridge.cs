using UnityEngine;
using UnityEngine.Events;

public class EventBridge : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onCall;

    public void Call()
    {
        onCall?.Invoke();
    }
}

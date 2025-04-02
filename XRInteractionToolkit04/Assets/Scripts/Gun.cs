using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onGrab;
    [SerializeField]
    private UnityEvent onRelease;

    public void Grab(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject;

        if(interactor is NearFarInteractor)
        {
            onGrab?.Invoke();
        }
    }

    public void Release(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject;

        if(interactor is NearFarInteractor)
        {
            onRelease?.Invoke();
        }
    }
}

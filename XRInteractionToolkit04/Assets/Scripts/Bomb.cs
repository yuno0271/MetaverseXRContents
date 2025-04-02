using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Bomb : MonoBehaviour
{
    public enum State { Idle, Drop}

    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private LayerMask explosionHIttableMask;
    [SerializeField]
    private float recycleDelay = 1.0f;
    [SerializeField]
    private UnityEvent onExplosion;
    [SerializeField]
    private UnityEvent onRecycle;

    private State state;

    public void Drop()
    {
        state = State.Drop;
    }

    public void Throw()
    {
        XRGrabInteractable interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0, 150, 300));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Idle) return;

        Explosion();
    }

    private void Explosion()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, explosionRadius,
            explosionHIttableMask, QueryTriggerInteraction.Collide);

        foreach(var overlap in overlaps)
        {
            if(overlap.TryGetComponent<Hittable>(out var hitObject))
            {
                hitObject.Hit();
            }
        }

        onExplosion?.Invoke();

        Invoke(nameof(Recycle), recycleDelay);
    }

    private void Recycle()
    {
        state = State.Idle;

        onRecycle?.Invoke();
    }
}

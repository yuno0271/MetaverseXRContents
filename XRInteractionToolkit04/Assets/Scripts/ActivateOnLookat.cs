using UnityEngine;

public class ActivateOnLookat : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private Behaviour target;
    [SerializeField]
    private float thresholdAngle = 30.0f;
    [SerializeField]
    private float thresholdDuration = 2.0f;

    private bool isLooking = false;
    private float showingTime;

    private void Awake()
    {
        target.enabled = false;
    }

    private void Update()
    {
        Vector3 dir = target.transform.position - camera.transform.position;
        float angle = Vector3.Angle(camera.transform.forward, dir);

        if (angle <= thresholdAngle)
        {
            if (!isLooking)
            {
                isLooking = true;
                showingTime = Time.time + thresholdDuration;
            }
            else
            {
                if (!target.enabled && Time.time >= showingTime)
                {
                    target.enabled = true;
                }
            }
        }
        else
        {
            if (isLooking)
            {
                isLooking = false;
                target.enabled = false;
            }
        }
    }
}

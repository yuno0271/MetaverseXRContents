using UnityEngine;

public class ChangeLinePosition : MonoBehaviour
{
    [SerializeField]
    private int index;
    private LineRenderer target;

    private void Awake()
    {
        target = GetComponent<LineRenderer>();
    }

    public void Call(Vector3 worldPosition)
    {
        if(target.useWorldSpace)
        {
            target.SetPosition(index, worldPosition);
        }
        else
        {
            Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
            target.SetPosition(index, localPosition);
        }
    }
}

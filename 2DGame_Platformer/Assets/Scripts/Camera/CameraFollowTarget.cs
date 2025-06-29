using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool x, y, z;

    private float offsetY;

    private void Awake()
    {
        offsetY = Mathf.Abs(transform.position.y - target.position.y);
    }

    private void LateUpdate()
    {
        // true�� �ุ target�� ��ǥ�� ���󰡵��� ����
        transform.position = new Vector3((x ? target.position.x : transform.position.x),
                                         (y ? target.position.y + offsetY : transform.position.y),
                                         (z ? target.position.z : transform.position.z));

        // ī�޶��� ��/���� �̵� ������ �Ѿ�� �ʵ��� ����
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(transform.position.x, stageData.CameraLimitMinX, stageData.CameraLimitMaxX);
        transform.position = position;
    }
}

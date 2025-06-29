using UnityEngine;

[CreateAssetMenu(menuName = "StageData", fileName = "New StageData")]
public class StageData : ScriptableObject
{
    [Header("Camera Limit")]
    [SerializeField]
    private float cameraLimitMinX;  // ���� ������������ ī�޶� �� �� �ִ� �ּ� x ��ġ (����)
    [SerializeField]
    private float cameraLimitMaxX;  // ���� ������������ ī�޶� �� �� �ִ� �ִ� x ��ġ (������)

    [Header("Player Limit")]
    [SerializeField]
    private float playerLimitMinX;  // ���� ������������ �÷��̾ �� �� �ִ� �ּ� x ��ġ (����)
    [SerializeField]
    private float playerLimitMaxX;  // ���� ������������ �÷��̾ �� �� �ִ� �ִ� x ��ġ (������)

    [Header("Map Limit")]
    [SerializeField]
    private float mapLimitMinY;     // ���� ���������� ���� �Ʒ� �ٴ� y ��ġ

    // �ܺο��� ���� �����Ϳ� �����ϱ� ���� ������Ƽ get
    public float CameraLimitMinX => cameraLimitMinX;
    public float CameraLimitMaxX => cameraLimitMaxX;
    public float PlayerLimitMinX => playerLimitMinX;
    public float PlayerLimitMaxX => playerLimitMaxX;
    public float MapLimitMinY => mapLimitMinY;
}

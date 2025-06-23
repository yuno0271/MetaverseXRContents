using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode jumpKeyCode = KeyCode.C;

    private MovementRigidbody2D movement;
    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        movement = GetComponent<MovementRigidbody2D>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    private void Update()
    {
        // Ű �Է� (��/�� ���� Ű, ���� Shift Ű)
        float x = Input.GetAxisRaw("Horizontal");
        float offset = 0.5f + Input.GetAxisRaw("Sprint") * 0.5f;

        // �ȱ��� �� ���� ������ -0.5 ~ 0.5
        // �ٱ��� �� ���� ������ -1 ~ 1�� ����
        x *= offset;

        // �÷��̾��� �̵� ���� (��/��)
        UpdateMove(x);
        // �÷��̾��� ���� ����
        UpdateJump();
        // �÷��̾� �ִϸ��̼� ����
        playerAnimator.UpdateAnimation(x);
    }

    private void UpdateMove(float x)
    {
        // �÷��̾��� ������ �̵� (��/��)
        movement.MoveTo(x);
    }

    private void UpdateJump()
    {
        if(Input.GetKeyDown(jumpKeyCode))
        {
            movement.Jump();
        }

        if(Input.GetKey(jumpKeyCode))
        {
            movement.IsLongJump = true;
        }
        else if(Input.GetKeyUp(jumpKeyCode))
        {
            movement.IsLongJump = false;
        }
    }
}

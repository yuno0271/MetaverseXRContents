using UnityEngine;

public class MovementEffects : MonoBehaviour
{
    private MovementRigidbody2D movement;

    // �÷��̾ �̵��� �� �� �ؿ� ������ ����Ʈ
    [SerializeField]
    private ParticleSystem footStepEffect;
    private ParticleSystem.EmissionModule footEmission;

    // �÷��̾ ���߿��� �ٴ����� ������ �� ������ ����Ʈ
    [SerializeField]
    private ParticleSystem landingEffect;
    private bool wasOnGround;

    private void Awake()
    {
        movement = GetComponentInParent<MovementRigidbody2D>();
        footEmission = footStepEffect.emission;
    }

    private void Update()
    {
        // �÷��̾ �ٴ��� ��� �ְ�, ��/�� �̵��ӵ��� 0�� �ƴϸ�
        if(movement.IsGrounded && movement.Velocity.x != 0)
        {
            footEmission.rateOverTime = 30f;
        }
        else
        {
            footEmission.rateOverTime = 0;
        }

        // �ٷ� ���� �����ӿ� ���߿� �־���, �̹� �����ӿ� �ٴ��� ��� �ְ�, 
        // y �ӷ��� 0 ������ �� �ٴڿ� "����"�� �Ǵ��ϰ� ����Ʈ ���
        if(!wasOnGround && movement.IsGrounded && movement.Velocity.y <= 0)
        {
            landingEffect.Stop();
            landingEffect.Play();
        }

        wasOnGround = movement.IsGrounded;
    }
}

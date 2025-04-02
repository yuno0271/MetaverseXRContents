using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    [SerializeField]
    private float chargingTime = 2.0f;
    [SerializeField]
    private int maxAmmo = 20;
    private int currentAmmo;

    [SerializeField]
    private UnityEvent onReloadStart;
    [SerializeField]
    private UnityEvent onReloadEnd;
    [SerializeField]
    private UnityEvent<int> onAmmoChanged;
    [SerializeField]
    private UnityEvent<float> onChargeChanged;

    private int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            currentAmmo = Mathf.Clamp(value, 0, maxAmmo);

            onAmmoChanged?.Invoke(currentAmmo);
            onChargeChanged?.Invoke((float)currentAmmo / maxAmmo);
        }
    }

    private void Start()
    {
        CurrentAmmo = maxAmmo;
    }

    public bool Use(int amount = 1)
    {
        if(CurrentAmmo >= amount)
        {
            CurrentAmmo-= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartReload()
    {
        if (CurrentAmmo == maxAmmo) return;

        StopAllCoroutines();
        StartCoroutine(nameof(ReloadProcess));
    }

    public void StopReload()
    {
        StopAllCoroutines();
    }

    private IEnumerator ReloadProcess()
    {
        onReloadStart?.Invoke();

        float beginTime = Time.time;
        int beginAmmo = currentAmmo;
        float enoughPercent = 1 - ((float)currentAmmo / maxAmmo);
        float enoughChargingTime = chargingTime * enoughPercent;

        while(true)
        {
            float t = (Time.time - beginTime) / enoughChargingTime;

            if (t >= 1) break;

            CurrentAmmo = (int)Mathf.Lerp(beginAmmo, maxAmmo, t);

            yield return null;
        }

        CurrentAmmo = maxAmmo;

        onReloadEnd?.Invoke();
    }
}

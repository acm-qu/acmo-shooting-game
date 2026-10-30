using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invincibilityTime = 1f;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public bool IsDead => CurrentHealth <= 0;

    public event Action<int, int> OnHealthChanged; // current, max
    public event Action OnDamaged;
    public event Action OnDied;

    private float invincibleUntil;

    private void Awake() => CurrentHealth = maxHealth;

    public void TakeDamage(int amount = 1)
    {
        if (IsDead || Time.time < invincibleUntil) return;

        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);
        invincibleUntil = Time.time + invincibilityTime;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (IsDead) OnDied?.Invoke();
        else OnDamaged?.Invoke();
    }
}
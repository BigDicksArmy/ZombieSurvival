using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Slider HealthBar;
    public float MaxHealth;
    public float CurrentHealth;

    private void Start()
    {
        HealthBar.minValue = 0;
        HealthBar.maxValue = MaxHealth;
        HealthBar.value = MaxHealth;
        CurrentHealth = MaxHealth;
    }
    public void DamagePlayer(float Damage)
    {
        CurrentHealth -= Damage;
    }
    void PlayerDeath()
    {
        if (CurrentHealth <= 0)
        {

        }
    }
}

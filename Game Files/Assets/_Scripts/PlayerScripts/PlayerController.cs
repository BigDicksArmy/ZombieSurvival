using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    private static PlayerController _instance;

    public static PlayerController Instance
    {
        get
        { return _instance; }

        private set
        { _instance = value; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("More than one instance of a singleton detected");
            return;
        }
        _instance = this;
    }
    #endregion

    public Slider HealthBar;

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    #region Properties
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth = value;
        }
    }
    #endregion
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
        HealthBar.value = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);    //make the player die on screen or disable controlls and play an animation, destroying may cause some trouble with references
        }
    }
}

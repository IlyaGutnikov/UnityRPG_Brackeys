using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth { get; private set; }

    public Stat Damage;
    public Stat Armor;

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }
#endif

    public void TakeDamage(int damage)
    {
        damage -= Armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        CurrentHealth -= damage;
        Debug.Log(transform.name + " takes damage: " + damage);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died.");
    }
}

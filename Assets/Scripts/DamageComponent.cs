using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    void Start()
    {
        m_CurrentHP = MaxHP;
    }

    void TakeDamage(int damage)
    {
        if (m_CurrentHP <= 0)
        {
            return;
        }

        m_CurrentHP -= damage;
        if (m_CurrentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Tour tower = GetComponent<Tour>();
        if (tower != null)
        {
            tower.Die();
        }

        Mob mob = GetComponent<Mob>();
        if (mob != null)
        {
            mob.Die();
        }
    }

    public int MaxHP;

    private int m_CurrentHP;
}
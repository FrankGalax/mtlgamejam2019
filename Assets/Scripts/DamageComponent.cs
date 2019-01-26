using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    void Start()
    {
        m_CurrentHP = MaxHP;
    }

    void OnGUI()
    {
        if (DisplayHealthBar)
        {
            float ratio = (float)m_CurrentHP / (float)MaxHP;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
            screenPosition.y = Screen.height - screenPosition.y;
            float sizeX = ratio * 50.0f;
            screenPosition.x -= sizeX / 2.0f;
            Rect rect = new Rect(screenPosition, new Vector2(sizeX, 5));
            GUI.DrawTexture(rect, ResourceManager.GetTexture("healthbar"));
        }
    }

    public void TakeDamage(int damage)
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
    public bool DisplayHealthBar;

    private int m_CurrentHP;
}
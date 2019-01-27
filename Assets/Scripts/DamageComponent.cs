using UnityEngine;
using System.Collections;

public enum DamageType
{
    Straw,
    Wood,
    Rock,
    Mob,
    Wolf
}

public class DamageComponent : MonoBehaviour
{
    void Start()
    {
        m_CurrentHP = MaxHP;
        m_LastGUIRatio = 1.0f;
    }

    void OnGUI()
    {
        if (DisplayHealthBar)
        {
            Tour tower = GetComponent<Tour>();
            if (tower != null && tower.IsBuilding)
            {
                return;
            }

            m_LastGUIRatio = Mathf.Lerp(m_LastGUIRatio, (float)m_CurrentHP / (float)MaxHP, 6.0f * Time.deltaTime);
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * HealthBarOffset);
            screenPosition.y = Screen.height - screenPosition.y;
            float sizeX = m_LastGUIRatio * 50.0f;
            screenPosition.x -= sizeX / 2.0f;
            Rect rect = new Rect(screenPosition, new Vector2(sizeX, 5));
            GUI.DrawTexture(rect, ResourceManager.GetTexture("healthbar"));
        }
    }

    public void TakeDamage(int damage, GameObject instigator, DamageType damageType)
    {
        if (damageType == DamageType.Rock && ResistRock)
        {
            return;
        }

        if (m_CurrentHP <= 0)
        {
            return;
        }

        m_CurrentHP -= damage;
        if (m_CurrentHP <= 0)
        {
            m_CurrentHP = 0;
            Die(instigator);
        }
    }

    private void Die(GameObject instigator)
    {
        Tour tower = GetComponent<Tour>();
        Mob instigatorMob = instigator.GetComponent<Mob>();
        if (instigatorMob != null && tower != null)
        {
            instigatorMob.OnTowerKill(tower);
        }

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
        }

        PathComponent pathComponent = GetComponent<PathComponent>();
        if (pathComponent != null)
        {
            pathComponent.enabled = false;
        }

        StartCoroutine(DeathAnim());
    }

    private IEnumerator DeathAnim()
    {
        float timer = 0.0f;
        Quaternion target = Quaternion.Euler(-90, 0, 0);
        while (timer < 1.0f)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.fixedDeltaTime;
            transform.rotation = Quaternion.Slerp(Quaternion.identity, target, timer / 1.0f);
        }
        Destroy(gameObject);
    }

    public int MaxHP;
    public bool DisplayHealthBar;
    public float HealthBarOffset = 1.75f;
    public bool ResistRock = false;

    private int m_CurrentHP;
    private float m_LastGUIRatio;
}
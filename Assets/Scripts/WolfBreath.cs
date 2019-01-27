using UnityEngine;

public class WolfBreath : MonoBehaviour
{
    void Update()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
        if (transform.position.x <= KillX)
        {
            Destroy(gameObject);
            return;
        }

        Tour[] towers = FindObjectsOfType<Tour>();
        foreach (Tour tower in towers)
        {
            if (tower.m_RessourceType != RessourceType.RessourceType_Rock && tower.transform.position.x >= transform.position.x)
            {
                DamageComponent damageComponent = tower.GetComponent<DamageComponent>();
                if (damageComponent != null)
                {
                    damageComponent.TakeDamage(1000, gameObject);
                }
            }
        }
    }

    public float Speed = 1.0f;
    public float KillX = -10.0f;
}
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
            if (tower.transform.position.x >= transform.position.x &&
                tower.transform.position.x <= transform.position.x + 1 &&
                !tower.IsProtectedFromWolf())
            {
                DamageComponent damageComponent = tower.GetComponent<DamageComponent>();
                if (damageComponent != null)
                {
                    damageComponent.TakeDamage(1000, gameObject, DamageType.Wolf);
                }
            }
        }
    }

    public float Speed = 1.0f;
    public float KillX = -10.0f;
}
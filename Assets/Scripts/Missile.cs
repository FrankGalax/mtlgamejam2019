using UnityEngine;

public class Missile : MonoBehaviour
{
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (m_Rigidbody != null)
        {
            m_Rigidbody.MovePosition(transform.position + Vector3.right * Speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        DamageComponent damageComponent = collision.collider.GetComponent<DamageComponent>();
        if (damageComponent != null)
        {
            damageComponent.TakeDamage(Damage, gameObject);
        }

        Destroy(gameObject);
    }

    public float Speed = 3.0f;
    public int Damage = 1;

    private Rigidbody m_Rigidbody;
}
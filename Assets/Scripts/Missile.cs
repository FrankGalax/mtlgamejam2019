using UnityEngine;

public class Missile : MonoBehaviour
{
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_CurrentPiercingCount = PiercingCount;
    }

    void FixedUpdate()
    {
        if (m_Rigidbody != null)
        {
            m_Rigidbody.MovePosition(transform.position + Vector3.right * Speed * Time.deltaTime);
        }

        if (transform.position.x > 20)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (UseCollisions)
        {
            ProcessCollision(collision.collider);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!UseCollisions)
        {
            ProcessCollision(collider);
        }
    }

    private void ProcessCollision(Collider collider)
    {
        DamageComponent damageComponent = collider.GetComponent<DamageComponent>();
        if (damageComponent == null && collider.transform.parent != null)
        {
            damageComponent = collider.transform.parent.GetComponent<DamageComponent>();
        }

        if (damageComponent != null)
        {
            damageComponent.TakeDamage(Damage, gameObject);
        }

        m_CurrentPiercingCount--;
        if (m_CurrentPiercingCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public float Speed = 3.0f;
    public int Damage = 1;
    public int PiercingCount = 1;
    public bool UseCollisions = true;

    private Rigidbody m_Rigidbody;
    private int m_CurrentPiercingCount;
}
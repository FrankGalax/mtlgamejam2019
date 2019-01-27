using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        m_CurrentPiercingCount = PiercingCount;
        m_IsDead = false;
    }

    void FixedUpdate()
    {
        if (m_Rigidbody != null && !m_IsDead)
        {
            m_Rigidbody.MovePosition(transform.position + Vector3.right * Speed * Time.deltaTime);
        }

        if (transform.position.x > 9.0f)
        {
            m_IsDead = true;
            GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(DeathAnim());
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

    private IEnumerator DeathAnim()
    {
        float timer = 0;
        while (timer < 0.5f)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
            transform.position -= Vector3.up * Time.fixedDeltaTime;
        }
        Destroy(gameObject);
    }

    public float Speed = 3.0f;
    public int Damage = 1;
    public int PiercingCount = 1;
    public bool UseCollisions = true;

    private Rigidbody m_Rigidbody;
    private int m_CurrentPiercingCount;
    private bool m_IsDead;
}
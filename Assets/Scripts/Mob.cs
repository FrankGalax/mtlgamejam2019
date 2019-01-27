using UnityEngine;

public class Mob : MonoBehaviour
{
    void Awake()
    {
        m_Graphics = transform.Find("Graphics");
    }
    
    void Start()
    {
        m_State = State.Moving;
    }

    void Update()
    {
        RaycastHit hit;
        int towerMask = 1 << LayerMask.NameToLayer("Tower");
        GameObject facingTower = null;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.left, out hit, AttackRange, towerMask))
        {
            facingTower = hit.collider.gameObject;
        }

        switch (m_State)
        {
            case State.Moving:
                Moving(facingTower);
                break;
            case State.Attacking:
                Attacking(facingTower);
                break;
        }
    }

    public void OnPathComplete()
    {
        Destroy(gameObject);
        Game.Instance.LooseHouseHealth();
        FindObjectOfType<House>().LooseHealth();
    }

    public void OnTowerKill(Tour tower)
    {
        if (Lane != null)
        {
            Lane.RemoveTower(tower);
        }
    }

    private void Moving(GameObject facingTower)
    {
        if (m_Graphics != null)
        {
            m_GraphicsTimer += Time.deltaTime;

            float sin = WiggleAmplitude * Mathf.Sin(WiggleSpeed * m_GraphicsTimer);
            Quaternion quaternion = m_Graphics.rotation;
            quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, sin);
            m_Graphics.rotation = quaternion;
        }

        if (facingTower != null)
        {
            m_State = State.Attacking;
            PathComponent pathComponent = GetComponent<PathComponent>();
            if (pathComponent != null)
            {
                pathComponent.FollowPath = false;
            }
            Rigidbody rigidBody = GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.isKinematic = true;
            }
            m_AttackTimer = AttackTime;
        }
    }

    private void Attacking(GameObject facingTower)
    {
        if (m_Graphics != null)
        {
            m_Graphics.transform.rotation = Quaternion.Slerp(m_Graphics.transform.rotation, Quaternion.identity, 3.0f * Time.deltaTime);
        }

        m_AttackTimer -= Time.deltaTime;
        if (m_AttackTimer < 0)
        {
            if (facingTower != null)
            {
                m_AttackTimer = AttackTime;
                DamageComponent damageComponent = facingTower.GetComponent<DamageComponent>();
                if (damageComponent != null)
                {
                    damageComponent.TakeDamage(AttackDamage, gameObject);
                }
            }
        }

        if (facingTower == null)
        {
            m_State = State.Moving;
            PathComponent pathComponent = GetComponent<PathComponent>();
            if (pathComponent != null)
            {
                pathComponent.FollowPath = true;
            }
            Rigidbody rigidBody = GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.isKinematic = false;
            }
        }
    }

    public float WiggleSpeed;
    public float WiggleAmplitude;
    public float AttackRange = 1.0f;
    public float AttackTime = 0.5f;
    public int AttackDamage = 2;

    public Lane Lane { get; set; }

    private enum State
    {
        Moving,
        Attacking
    }

    private Transform m_Graphics;
    private float m_GraphicsTimer;
    private State m_State;
    private float m_AttackTimer;
}
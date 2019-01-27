using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    void Awake()
    {
        m_Graphics = transform.Find("Graphics");
    }
    
    void Start()
    {
        m_State = State.Spawning;
        m_SpawnTimer = SpawnTime;
        m_IsDone = false;
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

        if (!m_IsDone)
        {
            switch (m_State)
            {
                case State.Spawning:
                    Spawning();
                    break;
                case State.Moving:
                    Moving(facingTower);
                    break;
                case State.Attacking:
                    Attacking(facingTower);
                    break;
            }
        }
        else
        {
            if (m_Graphics != null)
            {
                m_Graphics.transform.rotation = Quaternion.Slerp(m_Graphics.transform.rotation, Quaternion.identity, 3.0f * Time.deltaTime);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        m_State = State.Attacking;
        m_AttackTimer = AttackTime;
    }

    public void Setup(float endY)
    {
        m_StartY = transform.position.y;
        m_EndY = endY;
        DamageComponent damageComponent = GetComponent<DamageComponent>();
        if (damageComponent != null)
        {
            damageComponent.enabled = false;
        }
    }
    
    public void OnPathComplete()
    {
        m_IsDone = true;
        DamageComponent damageComponent = GetComponent<DamageComponent>();
        if (damageComponent != null)
        {
            damageComponent.enabled = false;
        }
        StartCoroutine(DeathAnim());
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

    private void Spawning()
    {
        m_SpawnTimer -= Time.deltaTime;
        
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            Vector3 start = new Vector3(transform.position.x, m_StartY, transform.position.z);
            Vector3 end = start;
            end.y = m_EndY;
            rigidbody.MovePosition(Vector3.Lerp(end, start, m_SpawnTimer / SpawnTime));
        }

        if (m_SpawnTimer < 0)
        {
            m_State = State.Moving;
            PathComponent pathComponent = GetComponent<PathComponent>();
            if (pathComponent != null)
            {
                pathComponent.FollowPath = true;
            }
            DamageComponent damageComponent = GetComponent<DamageComponent>();
            if (damageComponent != null)
            {
                damageComponent.enabled = true;
            }
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
            Tour facingTowerComponent = facingTower.GetComponent<Tour>();
            if (facingTowerComponent)
            {
                if (facingTowerComponent.m_TourType == TourType.TourType_AOE)
                {
                    return;
                }
            }

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

    private IEnumerator DeathAnim()
    {
        float timer = 0;
        while (timer < 3.0f)
        {
            yield return new WaitForFixedUpdate();
            timer += Time.deltaTime;
            transform.position -= Vector3.up * Time.fixedDeltaTime;
        }
        Destroy(gameObject);
    }

    public float WiggleSpeed;
    public float WiggleAmplitude;
    public float AttackRange = 1.0f;
    public float AttackTime = 0.5f;
    public int AttackDamage = 2;
    public float SpawnTime = 1.5f;

    public Lane Lane { get; set; }

    private enum State
    {
        Spawning,
        Moving,
        Attacking
    }

    private Transform m_Graphics;
    private float m_GraphicsTimer;
    private float m_SpawnTimer;
    private State m_State;
    private float m_AttackTimer;
    private bool m_IsDone;
    private float m_StartY;
    private float m_EndY;
}
using UnityEngine;

public class TourDamage : Tour
{
    public GameObject MissilePrefab;
    public float FireTime = 2.0f;
    public Vector3 MissileSpawnOffset;

    void Start()
    {
        m_FireTimer = -1.0f;
    }

    void Update()
    {
        m_FireTimer -= Time.deltaTime;

        if (m_FireTimer < 0)
        {
            RaycastHit hit;
            int mask = 1 << LayerMask.NameToLayer("Mob");
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right, out hit, 100.0f, mask))
            {
                Fire();
                m_FireTimer = FireTime;
            }
        }
    }

    private void Fire()
    {
        if (MissilePrefab == null)
        {
            return;
        }

        Instantiate(MissilePrefab, transform.position + MissileSpawnOffset, Quaternion.identity);
    }

    private float m_FireTimer;
}
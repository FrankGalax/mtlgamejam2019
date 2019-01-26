using UnityEngine;

public class Mob : MonoBehaviour
{
    public void OnPathComplete()
    {
        Destroy(gameObject);
    }
}
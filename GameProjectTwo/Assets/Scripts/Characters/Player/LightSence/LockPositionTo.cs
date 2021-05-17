using UnityEngine;

public class LockPositionTo : MonoBehaviour
{
    [SerializeField] Transform follow;
    void Update()
    {
        transform.position = follow.position - Vector3.up ;
    }
}

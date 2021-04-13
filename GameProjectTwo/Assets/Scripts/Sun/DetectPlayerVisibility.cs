 using UnityEngine;
 using System.Collections;

public class DetectPlayerVisibility : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float range = 1000f;
    [SerializeField] Transform earth;

    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;

        Vector3 dir = (transform.position - player.position).normalized;

        if (Physics.Raycast(rayOrigin, dir * -1, out hit, range))
        {
            if (hit.transform.tag == "Player")
            {
                print("player visible");
            }
            else
            {
                print("player not visible");
            }
        }

        transform.LookAt(earth);

    }

    private void OnDrawGizmos()
    {
        Vector3 dir = (transform.position - player.position).normalized;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, dir * -1 * range);
    }
}

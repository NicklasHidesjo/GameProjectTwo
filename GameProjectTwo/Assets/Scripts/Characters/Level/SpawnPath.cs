using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPath : MonoBehaviour
{
    [SerializeField] LayerMask checkLayers;
    [Tooltip("Use this to make the path reverse (makes it possible to copy a path and have it be walked backwards instead of making a brand new path")]
    [SerializeField] bool flippedPath;

    [Tooltip("True = guards will walk back the way they came, False = guards will loop")]
    [SerializeField] bool backTrack;
    public bool BackTrack => backTrack;

    [Tooltip("If this is a spawnPoint for a stationary civilian or guard")]
    [SerializeField] bool stationary;
    public bool Stationary => stationary;

    [SerializeField] int numOfGuards = 1;
    public int NumOfGuards => numOfGuards;

    [SerializeField] Transform spawnPos;
    public Transform SpawnPos => SpawnPos;

    //Call to get an array of pathpoints
    public List<PathPoint> GetPath()
    {
        List<PathPoint> path = new List<PathPoint>();

        //Randomize a point in each spawn area to create a path
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).position;
            Vector3 bounds = transform.GetChild(i).GetComponent<Renderer>().bounds.extents;
            pos += new Vector3(Mathf.Lerp(-bounds.x, bounds.x, Random.value), 0, Mathf.Lerp(-bounds.z, bounds.z, Random.value));

            RaycastHit hit;
            if (Physics.Raycast(pos + Vector3.up * 100, Vector3.down, out hit, 200, checkLayers, QueryTriggerInteraction.Ignore))
            {
                pos.y = hit.point.y + 1;
            }

            path.Add(new PathPoint(pos, transform.GetChild(i).GetComponent<PointGenerator>().IdlePoint));
        }

        if (flippedPath)
        {
                PathPoint[] newPath = new PathPoint[path.Count];
                int y = path.Count - 1;
                for (int x = 0; x < path.Count; x++)
                {
                    newPath[y] = path[x];
                    y--;
                }
                path = newPath.ToList();
        }
        return path;
    }
}

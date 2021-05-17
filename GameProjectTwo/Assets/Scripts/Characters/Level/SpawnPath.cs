using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnPath : MonoBehaviour
{
    [SerializeField] LayerMask checkLayers;
    [Tooltip("Use this to make the path reverse (makes it possible to copy a path and have it be walked backwards instead of making a brand new path")]
    [SerializeField] bool flippedPath;

    [SerializeField] PathPoint[] path;

    [Tooltip("True = guards will walk back the way they came, False = guards will loop")]
    [SerializeField] bool backTrack;
    public bool BackTrack => backTrack;

    [Tooltip("If this is a spawnPoint for a stationary civilian or guard")]
    [SerializeField] bool stationary;
    public bool Stationary => stationary;

    [Tooltip("The number of moving guards to spawn")]
    [SerializeField] int numOfGuards = 1;
    public int NumOfGuards => numOfGuards;

    [SerializeField] Transform spawnPos;
    public Transform SpawnPos => spawnPos;

    //Call to get an array of pathpoints
    public PathPoint[] GetPath()
    {
        if (flippedPath)
        {
            PathPoint[] newPath = new PathPoint[path.Length];
            int y = path.Length - 1;
            for (int x = 0; x < path.Length; x++)
            {
                newPath[y] = path[x];
                y--;
            }
            path = newPath;
        }
        return path;
    }
}

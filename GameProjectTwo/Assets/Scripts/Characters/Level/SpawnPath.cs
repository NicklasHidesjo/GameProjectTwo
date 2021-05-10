using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPath : MonoBehaviour
{
    [SerializeField] LayerMask checkLayers;
    [SerializeField] bool deathPoint;
    [SerializeField] bool active;
    [SerializeField] bool spawnGards;
    [SerializeField] bool spawnVillager;
    [SerializeField] bool randomStartPoint;
    [SerializeField] bool randomDirectionPoint;
    // Start is called before the first frame update
    void Start()
    {
        //TODO : FIX This
        SpawnNPCs(10);
    }

    //Get an Array of path and npcs
    void SpawnNPCs(int someNumber)
    {
        for (int i = 0; i < someNumber; i++)
        {
            Color type = GetTypeOfNPC();
            Vector3[] path = GetRandomizedPatrolPath();

            DrawPath(path, type, 10);
        }
    }

    //TODO : Get NPC type instead of color
    //Get NPC type to spawn
    Color GetTypeOfNPC()
    {
        if (spawnVillager && spawnGards)
        {
            if (Random.value > 0.5f)
            {
                return Color.cyan;
            }
            else
            {
                return Color.yellow;
            }
        }
        else if (spawnGards)
        {
            return Color.cyan;
        }
        else
        {
            return Color.yellow;
        }
    }

    void Grid()
    {
        //Grid
        Vector3[,] grid;
        Vector3 gridBounds;
        float CellSize = 10;


        Collider[] colliders = Resources.FindObjectsOfTypeAll(typeof(Collider)) as Collider[];

        float maxX = -Mathf.Infinity;
        float minX = Mathf.Infinity;
        float maxZ = -Mathf.Infinity;
        float minZ = Mathf.Infinity;

        float cMinX;
        float cMaxX;
        float cMinZ;
        float cMaxZ;
        Vector3 bounds;

        foreach (Collider col in colliders)
        {
            bounds = col.bounds.extents;
            cMinX = col.transform.position.x - bounds.x;
            cMaxX = col.transform.position.x + bounds.x;
            cMinZ = col.transform.position.z - bounds.z;
            cMaxZ = col.transform.position.z + bounds.z;

            if (cMaxX > maxX)
                maxX = cMaxX;

            if (cMinX < minX)
                minX = cMinX;

            if (cMinZ < minZ)
                minZ = cMinZ;

            if (cMaxZ > maxZ)
                maxZ = cMaxZ;
        }

        Vector3 ll = new Vector3(minX, 11, minZ);
        Vector3 lr = new Vector3(maxX, 11, minZ);
        Vector3 ul = new Vector3(minX, 11, maxZ);
        Vector3 ur = new Vector3(maxX, 11, maxZ);

        Debug.DrawLine(ll, lr, Color.red, 10);
        Debug.DrawLine(lr, ur, Color.red, 10);
        Debug.DrawLine(ur, ul, Color.red, 10);
        Debug.DrawLine(ul, ll, Color.red, 10);


        int xSize = (int)((maxX - minX) / CellSize);
        int zSize = (int)((maxZ - minZ) / CellSize);

        gridBounds = Vector3.one * CellSize;
        gridBounds.y = 0;

        grid = new Vector3[xSize, zSize];

        for (int x = 0; x < xSize; x++)
        {
            for (int z = 0; z < zSize; z++)
            {
                grid[x, z] = new Vector3(minX + gridBounds.x * x + CellSize, 0, minZ + gridBounds.z * z + CellSize);
                Debug.DrawLine(grid[x, z] + (Vector3.left + Vector3.forward) * gridBounds.x, grid[x, z] + (Vector3.right + Vector3.forward) * gridBounds.x, Color.yellow, 10);
                Debug.DrawLine(grid[x, z] + (Vector3.right + Vector3.forward) * gridBounds.x, grid[x, z] + (Vector3.right + Vector3.back) * gridBounds.x, Color.yellow, 10);
                Debug.DrawLine(grid[x, z] + (Vector3.right + Vector3.back) * gridBounds.x, grid[x, z] + (Vector3.left + Vector3.back) * gridBounds.x, Color.yellow, 10);
                Debug.DrawLine(grid[x, z] + (Vector3.left + Vector3.back) * gridBounds.x, grid[x, z] + (Vector3.left + Vector3.forward) * gridBounds.x, Color.yellow, 10);
            }
        }
    }


    //Call to get an array of pathpoints
    Vector3[] GetRandomizedPatrolPath()
    {
        Vector3[] path = new Vector3[transform.childCount];

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
            path[i] = pos;
        }

        //Randomize path direction
        if (randomDirectionPoint)
        {
            if (Random.value > 0.5f)
            {
                Vector3[] rewPath = new Vector3[path.Length];
                int y = path.Length - 1;
                for (int x = 0; x < path.Length; x++)
                {
                    rewPath[y] = path[x];
                    y--;
                }
                path = rewPath;
            }
        }

        //Randomize startPoint of array
        if (randomStartPoint)
        {
            int startAt = (int)Random.Range(0, path.Length);
            Vector3[] newOrderPath = new Vector3[path.Length];
            for (int x = 0; x < path.Length; x++)
            {
                if (startAt % (path.Length - 1) == 0)
                {
                    startAt = 0;
                }
                newOrderPath[startAt] = path[x];
                startAt++;
            }
        }


        return path;
    }


    void DrawPath(Vector3[] path, Color col, float t)
    {
        int x = 0;
        for (int y = 1; y < path.Length; y++)
        {
            DebugLineArrow(path[x], path[y], 1.25f, col * (1 - y * 0.2f), t);
            x = y;
        }
        DebugLineArrow(path[path.Length - 1], path[0], 1.25f, col * (1 - (path.Length) * 0.2f), t);

    }

    void DebugLineArrow(Vector3 start, Vector3 end, float headSize, Color col, float t)
    {
        Vector3 dir = end - start;
        dir = dir.normalized;
        Debug.DrawLine(start, end, col, t);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(45, 135, 0) * Vector3.up) * headSize, col, t);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(135, 135, 0) * Vector3.up) * headSize, col, t);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(225, 45, 0) * Vector3.up) * headSize, col, t);
        Debug.DrawLine(end, end + (Quaternion.LookRotation(dir) * Quaternion.Euler(315, 45, 0) * Vector3.up) * headSize, col, t);
    }
}

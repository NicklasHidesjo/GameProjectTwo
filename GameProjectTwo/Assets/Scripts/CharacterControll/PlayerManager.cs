using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Camera playerCam;
    [SerializeField] GameObject draculaPreFab;
    [SerializeField] GameObject batPreFab;
    [SerializeField] GameObject ragdollPrefab;

    PlayerState playerState;
    GameObject draculaGO;
    GameObject batGO;
    GameObject Ragdoll;

  
    /// TODO : Remove this function when Test is over
    public bool transformME;
    void TestTransform()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("transform me");

            transformME = !transformME;
            if (transformME)
                playerState.SetState(PlayerState.playerStates.TransformToBat);
            else
                playerState.SetState(PlayerState.playerStates.TransformToDracula);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
        SpawnNewPlayer();
        playerState.SetState(PlayerState.playerStates.TransformToDracula);
    }

    // Update is called once per frame
    void Update()
    {
        TestTransform();
    }

    private void FixedUpdate()
    {
        playerState.UpdateByState();
    }

    private void Init()
    {
        if (!spawnPoint)
            spawnPoint = transform.GetChild(0);

        playerState = GetComponent<PlayerState>();

        if (!playerCam)
            playerCam = Camera.main;

        playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    } 
    public void SpawnNewPlayer()
    {
        draculaGO = Instantiate(draculaPreFab, spawnPoint.position, Quaternion.identity);
        DraculaMovement draculaMovement = draculaGO.GetComponent<DraculaMovement>();
        draculaGO.SetActive(false);

        batGO = Instantiate(batPreFab, spawnPoint.position, Quaternion.identity);
        BatMovement batMovement = batGO.GetComponent<BatMovement>();
        batGO.SetActive(false);

        playerState.SetScipts(this, draculaMovement, batMovement);
    }

    public void DestroyPlayer()
    {
        Destroy(draculaGO);
        Destroy(batGO);
    }

    public void ActivateDracula()
    {
        batGO.SetActive(false);

        SetPooledActive(draculaGO);

        playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
        playerState.SetState(PlayerState.playerStates.MoveDracula);

    }

    public void ActivateBat()
    {
        draculaGO.SetActive(false);
        SetPooledActive(batGO);
        playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.high, spawnPoint);

        playerState.SetState(PlayerState.playerStates.FlyBat);
    }

    private void SetPooledActive(GameObject activateGO)
    {
        activateGO.transform.position = spawnPoint.position;
        activateGO.SetActive(true);
        spawnPoint.parent = activateGO.transform;
    }
}

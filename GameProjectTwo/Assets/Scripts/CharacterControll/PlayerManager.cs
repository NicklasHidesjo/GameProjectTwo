using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singelton :
//This is the playerManager it keeps decieds where and what should be spawned and inisialized
//This is closely linked to "PlayerState"

[RequireComponent(typeof(PlayerState))]
public class PlayerManager : MonoBehaviour
{
    // singleton
    public static PlayerManager instance;
   
    [SerializeField] Transform spawnPoint;
    [SerializeField] Camera playerCam;
    [SerializeField] GameObject draculaPreFab;
    [SerializeField] GameObject batPreFab;

    [SerializeField] HealthManager health;

    private PlayerState playerState;
    private GameObject draculaGO;
    private GameObject batGO;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
        SpawnNewPlayer();
        playerState.SetState(PlayerState.playerStates.TransformToDracula);
    }

    
    private void Init()
    {
        if (!spawnPoint)
            spawnPoint = transform.GetChild(0);

        spawnPoint.GetComponent<MeshRenderer>().enabled = false;

        if (!playerState)
            playerState = GetComponent<PlayerState>();

        if (!playerCam)
            playerCam = Camera.main;

       // playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
    }
    public void SpawnNewPlayer()
    {
        draculaGO = Instantiate(draculaPreFab, spawnPoint.position, Quaternion.identity);
        DraculaMovement draculaMovement = draculaGO.GetComponent<DraculaMovement>();
        draculaMovement.Init(playerState, playerCam.transform);

        draculaGO.GetComponent<PlayerObjectInteract>().playerState = playerState;
        
        
        draculaGO.SetActive(false);

        batGO = Instantiate(batPreFab, spawnPoint.position, Quaternion.identity);
        BatMovement batMovement = batGO.GetComponent<BatMovement>();
        batMovement.Init(playerState);
        batGO.SetActive(false);

        playerState.SetScipts(this, draculaMovement, batMovement);
    }

    // Update is called once per frame
    void Update()
    {
        //Update
    }

    private void FixedUpdate()
    {
        playerState.UpdateByState();
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void ActivateDracula()
    {
        batGO.SetActive(false);
        SetPooledActive(draculaGO);

       // playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
        playerState.SetState(PlayerState.playerStates.MoveDracula);
    }

    public void ActivateBat()
    {
        draculaGO.SetActive(false);
        SetPooledActive(batGO);
        
       // playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.high, spawnPoint);
        playerState.SetState(PlayerState.playerStates.FlyBat);
    }

    private void SetPooledActive(GameObject activateGO)
    {
        activateGO.transform.position = spawnPoint.position;
        Vector3 frw = spawnPoint.forward;
        frw.y = 0;
        spawnPoint.forward = frw.normalized;
        activateGO.transform.forward = spawnPoint.forward;
        activateGO.SetActive(true);
        spawnPoint.parent = activateGO.transform;
    }

    public void DestroyPlayer()
    {
        Destroy(draculaGO);
        Destroy(batGO);
    }
}

using System;
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
   
    [SerializeField] Transform playerPointTransform;
    [SerializeField] Camera playerCam;
    [SerializeField] GameObject draculaPreFab;
    [SerializeField] GameObject batPreFab;
    [SerializeField] GameObject smokePuff;
    [SerializeField] private PlayerStatsManager statsManager;
    public PlayerStatsManager StatsManager { get { return statsManager; } }

    [SerializeField] PlayerNotoriousLevels notoriusLevel;
    public PlayerNotoriousLevels NotoriousLevel { get { return notoriusLevel; } }

    [SerializeField] LightSensor lightSensor;

    private PlayerState playerState;
    public PlayerState PlayerState => playerState;

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
        GetHealthManager();
        SpawnNewPlayer();
        playerState.SetState(PlayerState.playerStates.TransformToDracula);
        EndLevelCheck.OnLevelEnded += PrepPlayerForNextLevel;
    }


    private void Init()
    {
        if (!playerPointTransform)
            playerPointTransform = transform.GetChild(0);

        playerPointTransform.GetComponent<MeshRenderer>().enabled = false;

        if (!playerState)
            playerState = GetComponent<PlayerState>();

        if (!playerCam)
            playerCam = Camera.main;

        if (!notoriusLevel)
        {
            notoriusLevel = GetComponent<PlayerNotoriousLevels>();
        }

        if (!lightSensor)
        {
            lightSensor = GetComponentInChildren<LightSensor>();
        }

        

       // playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
    }
    
    private void GetHealthManager()
    {
        statsManager = gameObject.GetComponent<PlayerStatsManager>();
    }

    

    public void SpawnNewPlayer()
    {
        draculaGO = Instantiate(draculaPreFab, playerPointTransform.position, Quaternion.identity);
        DraculaMovement draculaMovement = draculaGO.GetComponent<DraculaMovement>();
        draculaMovement.Init(playerState, playerCam.transform);

        draculaGO.GetComponent<PlayerObjectInteract>().playerState = playerState;
        
        
        draculaGO.SetActive(false);

        batGO = Instantiate(batPreFab, playerPointTransform.position, Quaternion.identity);
        BatMovement batMovement = batGO.GetComponent<BatMovement>();
        batMovement.Init(playerState);
        batGO.SetActive(false);

        playerState.SetScipts(this, draculaMovement, batMovement);
    }

    private void PrepPlayerForNextLevel(int newLevel)
    {
        //playerState.SetState(PlayerState.playerStates.DraculaHidden);
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

    public Transform GetPlayerPoint()
    {
        return playerPointTransform;
    }

    public void ActivateDracula()
    {
        GameObject puff = Instantiate(smokePuff, playerPointTransform.position, Quaternion.identity);
        Destroy(puff, 5);

        batGO.SetActive(false);
        SetPooledActive(draculaGO);
        AudioManager.instance.PlaySound(SoundType.DraculaTransform, draculaGO);

        // playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
        playerState.SetState(PlayerState.playerStates.DraculaDefault);
    }

    public void ActivateBat()
    {
        GameObject puff = Instantiate(smokePuff, playerPointTransform.position, Quaternion.identity);
        Destroy(puff, 5);

        draculaGO.SetActive(false);
        SetPooledActive(batGO);
        AudioManager.instance.PlaySound(SoundType.BatTransform, batGO);
       // playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.high, spawnPoint);
        playerState.SetState(PlayerState.playerStates.BatDefault);
    }
    

    private void SetPooledActive(GameObject activateGO)
    {
        activateGO.transform.position = playerPointTransform.position;
        Vector3 frw = playerPointTransform.forward;
        frw.y = 0;
        playerPointTransform.forward = frw.normalized;
        activateGO.transform.forward = playerPointTransform.forward;
        activateGO.SetActive(true);

        lightSensor.SetFollowTarget(activateGO.transform);

        playerPointTransform.parent = activateGO.transform;
    }

    public void DestroyPlayer()
    {
        Destroy(draculaGO);
        Destroy(batGO);
    }
}

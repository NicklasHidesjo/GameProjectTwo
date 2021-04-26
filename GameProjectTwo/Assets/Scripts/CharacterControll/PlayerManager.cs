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

    [SerializeField] public PlayerStatsManager stats { get { return stats; } }
    [SerializeField] public HealthManager health { get { return health; } }

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

       // playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
    }
    
    private void GetHealthManager()
    {
        gameObject.GetComponent<PlayerStatsManager>();
        gameObject.GetComponent<HealthManager>();
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

    public void AddDamage(int dmg)
    {
        health.LoseHealth(dmg);
    }
    public void loseSTM(float loss)
    {
        stats.DecreaseStaminaValue(loss);
    }

    public float GetCurrentStamina()
    {
        return 1;
        //stats.CurrentStamina;
    }

    private void SetPooledActive(GameObject activateGO)
    {
        activateGO.transform.position = playerPointTransform.position;
        Vector3 frw = playerPointTransform.forward;
        frw.y = 0;
        playerPointTransform.forward = frw.normalized;
        activateGO.transform.forward = playerPointTransform.forward;
        activateGO.SetActive(true);
        playerPointTransform.parent = activateGO.transform;
    }

    public void DestroyPlayer()
    {
        Destroy(draculaGO);
        Destroy(batGO);
    }
}

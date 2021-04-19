using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the playerManager it keeps decieds where and what should be spawned and inisialized
//This is closely linked to "PlayerState"
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(InSunLight))]
[RequireComponent(typeof(PlayerNotoriousLevels))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] Camera playerCam;
    [SerializeField] GameObject draculaPreFab;
    [SerializeField] GameObject batPreFab;

    [SerializeField] float draculaSuspicionLevel = 1;
    [SerializeField] float batSuspicionLevel = 0.5f;

    private PlayerNotoriousLevels notoriousLevel;
    private InSunLight inSun;
    private PlayerState playerState;
    private GameObject draculaGO;
    private GameObject batGO;

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

        if (!notoriousLevel)
            notoriousLevel = GetComponent<PlayerNotoriousLevels>();

        if (!inSun)
            inSun = GetComponent<InSunLight>();

        if (!playerState)
            playerState = GetComponent<PlayerState>();

        if (!playerCam)
            playerCam = Camera.main;

        playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
    }
    public void SpawnNewPlayer()
    {
        draculaGO = Instantiate(draculaPreFab, spawnPoint.position, Quaternion.identity);
        DraculaMovement draculaMovement = draculaGO.GetComponent<DraculaMovement>();
        draculaMovement.Init(playerState, playerCam.transform);
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
        DebugPlayerNotoriousLevels();
    }

    private void FixedUpdate()
    {
        playerState.UpdateByState();

        if (inSun.IsInSun())
        {
            notoriousLevel.SetPlLuminosity(1);
        }
        else
        {
            notoriousLevel.SetPlLuminosity(0.25f);
        }
    }

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public void ActivateDracula()
    {
        batGO.SetActive(false);
        SetPooledActive(draculaGO);


        notoriousLevel.SetPlSuspiusLevel(draculaSuspicionLevel);

        playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.low, spawnPoint);
        playerState.SetState(PlayerState.playerStates.MoveDracula);
    }

    public void ActivateBat()
    {
        draculaGO.SetActive(false);
        SetPooledActive(batGO);

        notoriousLevel.SetPlSuspiusLevel(batSuspicionLevel);

        playerCam.GetComponent<CameraController>().SetNewTarget(CameraController.cameraPriority.high, spawnPoint);
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

        inSun.SetDracula(activateGO.transform);
    }

    public void DestroyPlayer()
    {
        Destroy(draculaGO);
        Destroy(batGO);
    }

    void DebugPlayerNotoriousLevels()
    {
        float range = notoriousLevel.GetPlayerNotoriousLevel() * 10;

        Vector3 Eangel = Vector3.zero;
        Vector3[] p = new Vector3[36];

        int x = p.Length - 1;
        p[x] = (Vector3.forward * range);
        for (int i = 0; i < p.Length; i++)
        {
            Eangel.y = 10 * i;
            p[i] = Quaternion.Euler(Eangel) * (Vector3.forward * range);
            Debug.DrawLine(spawnPoint.position + p[x], spawnPoint.position + p[i]);
            x = i;
        }
    }
}

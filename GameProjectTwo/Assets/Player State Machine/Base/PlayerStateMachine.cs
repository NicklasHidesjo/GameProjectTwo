using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] PlayerState startingState;
    [SerializeField] private PlayerState currentState;
    private IPlayer player;

    private void Start()
    {
        player = GetComponent<IPlayer>();
        SetState(startingState);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.ExecuteState();
        }
    }

    public void SetState(PlayerState newState)
    {
        if(newState == null) 
	{ 
		Debug.LogError("No state found"); 
		return; 
	}
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this, player);
    }
}

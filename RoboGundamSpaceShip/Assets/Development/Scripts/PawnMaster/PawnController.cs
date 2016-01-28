using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class PawnController : NetworkBehaviour {
	#region Public Variables
	public float PLAYER_MOVE_MULTIPLIER = 0.05f; //make a const later
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	[SyncVar] private Vector2 m_playerPosition; //synchronized over the network
	
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_playerPosition = transform.position;
		//spawn local Camera
		
	}
	//runs every frame
	public void Update()
	{
		if (!isLocalPlayer) //is this my player? return if its not mine
		{
			return;
		}
			
		
		//placeholder movement
		if (Input.GetKey(KeyCode.UpArrow))
			m_playerPosition += (new Vector2(0, 1) * PLAYER_MOVE_MULTIPLIER);
		else if (Input.GetKey(KeyCode.DownArrow))
			m_playerPosition += (new Vector2(0, -1) * PLAYER_MOVE_MULTIPLIER);
		if (Input.GetKey(KeyCode.RightArrow))
			m_playerPosition += (new Vector2(1, 0) * PLAYER_MOVE_MULTIPLIER);
		else if (Input.GetKey(KeyCode.LeftArrow))
			m_playerPosition += (new Vector2(-1, 0) * PLAYER_MOVE_MULTIPLIER);

		transform.position = m_playerPosition;
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

// Script controls the players ability to take control of 
// network gameobjects
// Written by: Adam Bysice
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnterAbility : NetworkBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private PawnController m_pawn;
	private GameObject m_enterable;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_pawn = gameObject.GetComponent<PawnController>();
	}
	//runs every frame
	public void Update()
	{
		//request to enter/unenter an object
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (m_enterable)
			{
				//send request to server asking to get in or out
				CmdRequestToEnter();
			}
		}
	}
	#endregion

	#region Public Methods
	public void UpdateEnterable(GameObject p_actor)
	{
		if (!isServer)
			return;

		m_enterable = p_actor;

		if (m_enterable == null)
			RpcClearEnterable();
		else
			RpcUpdateEnterable(p_actor.GetComponent<NetworkIdentity>().netId);
	}

	//update "enterable" for local players
	[ClientRpc]
	public void RpcUpdateEnterable(NetworkInstanceId p_id)
	{
		Debug.Log("Updating the enterable");
		m_enterable = ClientScene.FindLocalObject(p_id);
	}
	//clear any "enterable" objects for the client
	[ClientRpc]
	public void RpcClearEnterable()
	{
		Debug.Log("Clearing the enterable");
		m_enterable = null;
	}

	[Command]
	public void CmdRequestToEnter()
	{
		//change ownership of the ship
		if (!m_pawn.m_isPiloting)
		{
			m_enterable.GetComponent<NetworkIdentity>().AssignClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
			Debug.Log("dick docked");
			//tell everyone to change the player state
			m_pawn.m_isPiloting = true;
		}
		else
		{
			m_enterable.GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
			Debug.Log("dick undocked");
			m_pawn.m_isPiloting = false;
		}

	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

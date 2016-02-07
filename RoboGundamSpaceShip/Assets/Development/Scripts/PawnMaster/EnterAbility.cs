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
	public PawnController m_pawn;
	public GameObject m_enterable;
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

	//request ownership of the gameobject
	[Command]
	public void CmdRequestToEnter()
	{
		//change ownership of the ship
		if (!m_pawn.isPiloting())
		{
			//add some sort of check to see if its already owned here
			m_enterable.GetComponent<NetworkIdentity>().AssignClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
			//Debug.Log("Controlling the ship");
			//tell everyone to change the player state
			m_pawn.RpcSetToPiloting(m_enterable.GetComponent<NetworkIdentity>().netId);
		}
		else
		{
			m_enterable.GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
			//Debug.Log("No longer controlling the ship");
			m_pawn.RpcUnpilotPawn();
		}

	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

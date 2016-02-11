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
	public GameObject[] m_turrets;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_pawn = gameObject.GetComponent<PawnController>();
		m_turrets = new GameObject[2];
	}
	//runs every frame
	public void Update()
	{
		//request to enter/unenter an object
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (m_enterable || (m_turrets[0] && m_turrets[1]))
			{
				//send request to server asking to get in or out
				CmdRequestToEnter();
			}
		}
	}
	#endregion

	#region Public Methods
	//updates the players "enterable" object 
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

	//request ownership of the gameobject that you are allowed to enter
	[Command]
	public void CmdRequestToEnter()
	{
		//change ownership of the object
		if (m_enterable)
		{ 
			if (!m_pawn.isPiloting())
			{
				NetworkIdentity l_id = gameObject.GetComponent<NetworkIdentity>();
				NetworkIdentity l_enterableid = m_enterable.GetComponent<NetworkIdentity>();
				//don't let them enter if someone is already in it
				if(l_enterableid.clientAuthorityOwner == null)
				{
					l_enterableid.AssignClientAuthority(l_id.connectionToClient);
					m_pawn.RpcSetToPiloting(l_enterableid.netId);
				}
			}
			else //make them get out
			{
				m_enterable.GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
				m_pawn.RpcUnpilotPawn();
			}
		}
		else
		{
			if (!m_pawn.isPiloting())
			{
				NetworkIdentity l_id = gameObject.GetComponent<NetworkIdentity>();
				NetworkIdentity l_enterableid1 = m_turrets[0].GetComponent<NetworkIdentity>();
				NetworkIdentity l_enterableid2 = m_turrets[1].GetComponent<NetworkIdentity>();
				//don't let them enter if someone is already in it
				if (l_enterableid1.clientAuthorityOwner == null && l_enterableid2.clientAuthorityOwner == null)
				{
					l_enterableid1.AssignClientAuthority(l_id.connectionToClient);
					l_enterableid2.AssignClientAuthority(l_id.connectionToClient);
					m_pawn.RpcSetToPiloting(l_enterableid1.netId);
				}
			}
			else //make them get out
			{
				m_turrets[0].GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
				m_turrets[1].GetComponent<NetworkIdentity>().RemoveClientAuthority(gameObject.GetComponent<NetworkIdentity>().connectionToClient);
				m_pawn.RpcUnpilotPawn();
			}
		}

	}
	/// <summary>
	/// TURRET CODE GOES HERE
	/// </summary>
	/// <param name="p_turret1"></param>
	/// <param name="p_turret2"></param>

	public void UpdateTurrets(GameObject p_turret1, GameObject p_turret2)
	{
		if (!isServer)
			return;

		m_turrets[0] = p_turret1;
		m_turrets[1] = p_turret2;


		if (m_turrets[0] == null && m_turrets[1] == null)
			RpcClearTurrets();
		else if(m_turrets[0] != null && m_turrets[1] != null)
			RpcUpdateTurrets(m_turrets[0].GetComponent<NetworkIdentity>().netId, m_turrets[1].GetComponent<NetworkIdentity>().netId);

	}

	[ClientRpc]
	public void RpcUpdateTurrets(NetworkInstanceId p_id, NetworkInstanceId p_id2)
	{
		Debug.Log("Updating the turrets");
		m_turrets[0] = ClientScene.FindLocalObject(p_id);
		m_turrets[1] = ClientScene.FindLocalObject(p_id2);
	}

	[ClientRpc]
	public void RpcClearTurrets()
	{
		Debug.Log("Clearing the turrets");
		m_turrets[0] = null;
		m_turrets[1] = null;
	}

	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

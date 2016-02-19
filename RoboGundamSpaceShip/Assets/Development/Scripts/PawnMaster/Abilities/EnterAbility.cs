// Script controls the players ability to take control of 
// network gameobjects
// Written by: Adam Bysice
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class EnterAbility : NetworkBehaviour {

	#region Public Variables

	public GameObject[] m_turrets;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private PawnController m_pawn;
	public List<GameObject> m_enterables;
	#endregion

	#region Accessors
	public int EnterablesCount()
	{
		return m_enterables.Count;
	}

	public Vector2 EnterablePos()
	{
		return m_enterables[0].transform.position;
	}
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_enterables = new List<GameObject>();
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
			if (m_enterables.Count > 0)
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

		if (p_actor == null)
			RpcClearEnterable();
		else
			RpcUpdateEnterable(p_actor.GetComponent<NetworkIdentity>().netId);
	}


	//update "enterable" for local players
	[ClientRpc]
	public void RpcUpdateEnterable(NetworkInstanceId p_id)
	{
		Debug.Log("Updating the enterable");
		//m_enterable = ClientScene.FindLocalObject(p_id);
		m_enterables.Add(ClientScene.FindLocalObject(p_id));
	}
	//clear any "enterable" objects for the client
	[ClientRpc]
	public void RpcClearEnterable()
	{
		Debug.Log("Clearing the enterable");
		//m_enterable = null;
		m_enterables.Clear();
	}

	//request ownership of the gameobject that you are allowed to enter
	[Command]
	public void CmdRequestToEnter()
	{
		//change ownership of the object
		if (m_enterables.Count > 0)
		{ 
			if (!m_pawn.isPiloting())
			{
				NetworkIdentity l_id = gameObject.GetComponent<NetworkIdentity>();
				foreach(GameObject obj in m_enterables)
				{
					NetworkIdentity l_enterableid = obj.GetComponent<NetworkIdentity>();
					//don't let them enter if someone is already in it
					if (l_enterableid.clientAuthorityOwner == null)
					{
						l_enterableid.AssignClientAuthority(l_id.connectionToClient);
						m_pawn.RpcSetToPiloting(l_enterableid.netId);
					}
				}
			}
			else //make them get out
			{
				foreach (GameObject obj in m_enterables)
				{
					IEnterable l_controller = (IEnterable)obj.GetComponent(typeof(IEnterable));
					l_controller.ServerPowerDown();
				}
				StartCoroutine(BlockWait());
			}
		}
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	IEnumerator BlockWait()
	{
	    yield return new WaitForSeconds(0.5f);
		NetworkIdentity l_id = gameObject.GetComponent<NetworkIdentity>();
		foreach (GameObject obj in m_enterables)
		{
			NetworkIdentity l_enterableid = obj.GetComponent<NetworkIdentity>();
			if (l_enterableid.clientAuthorityOwner != null)
				l_enterableid.RemoveClientAuthority(l_id.connectionToClient);
			
			IEnterable l_controller = (IEnterable)obj.GetComponent(typeof(IEnterable));
			l_controller.ServerFinishPowerDown();
		}
		m_pawn.RpcUnpilotPawn();
	}
	#endregion
}

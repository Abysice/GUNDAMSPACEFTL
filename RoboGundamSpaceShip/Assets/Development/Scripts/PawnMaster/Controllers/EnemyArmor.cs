// Script is base class for things that can be ripped off by the grapple ability
//
// Written By: Adam Bysice

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemyArmor : NetworkBehaviour, IDamageable {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private NetworkIdentity m_gundamID;
	private NetworkIdentity m_armorID;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	public void Start()
	{
		if (isServer)
		{
			m_armorID = gameObject.GetComponent<NetworkIdentity>();
			m_gundamID = Managers.GetInstance().GetPlayerManager().m_gundam.GetComponent<NetworkIdentity>();
		}
	}

	public void Update()
	{
		if (isServer)
		{
			if (m_gundamID.clientAuthorityOwner != null)
				m_armorID.AssignClientAuthority(m_gundamID.clientAuthorityOwner);
		}
	}
	#endregion

	#region Public Methods
	public void Damage(int damageTaken)
	{
		IDamageable l_target = (IDamageable)transform.parent.GetComponent(typeof(IDamageable));
		if (l_target != null)
			l_target.Damage(damageTaken);
	}

	[ClientRpc]
	public void RpcSetup(NetworkInstanceId p_parent)	
	{
		transform.parent = ClientScene.FindLocalObject(p_parent).transform;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

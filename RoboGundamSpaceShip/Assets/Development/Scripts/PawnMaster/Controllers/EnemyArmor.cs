// Script is base class for things that can be ripped off by the grapple ability
//
// Written By: Adam Bysice

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemyArmor : NetworkBehaviour, IDamageable {

	#region Public Variables
	public float m_MAX_RIP_DISTANCE;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private NetworkIdentity m_gundamID;
	private NetworkIdentity m_armorID;
	private Rigidbody2D m_rb;
	private SpringJoint2D m_spring;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	public void Start()
	{
		m_spring = gameObject.AddComponent<SpringJoint2D>();
		m_spring.breakForce = 1;
		m_spring.frequency = 0;
		m_spring.dampingRatio = 0;
		m_spring.connectedBody = transform.parent.Find("WeakSpot").GetComponent<Rigidbody2D>();
	
		if (isServer)
		{
			m_armorID = gameObject.GetComponent<NetworkIdentity>();
			m_gundamID = Managers.GetInstance().GetPlayerManager().m_gundam.GetComponent<NetworkIdentity>();
			m_rb = gameObject.GetComponent<Rigidbody2D>();
			m_MAX_RIP_DISTANCE = 10.0f;
		}
	}

	public void Update()
	{
		if (isServer)
		{
			if (m_gundamID.clientAuthorityOwner != null)
				m_armorID.AssignClientAuthority(m_gundamID.clientAuthorityOwner);

			if(m_rb.isKinematic == false)
			{
				float l_d = Vector2.Distance(transform.position, transform.parent.position);
				if (l_d > m_MAX_RIP_DISTANCE)
				{
					NetworkServer.Destroy(gameObject);
				}
			}
		}
	}
	#endregion

	#region Public Methods
	public void Damage(int damageTaken)
	{
		if (m_rb.isKinematic == false)
			return;

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

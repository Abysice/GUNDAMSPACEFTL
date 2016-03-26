// This script controls the mecha's grappling hook ability
// has some placeholder controls
// Written by: Adam Bysice
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GrappleAbility : NetworkBehaviour {

	#region Public Variables
	
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	private DistanceJoint2D m_hook;
	private GameObject m_connected;
	private LineRenderer m_cableLine;
	private Vector2 m_attachpoint;
	private NetworkIdentity m_id;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_cableLine = gameObject.GetComponent<LineRenderer>();
		m_id = gameObject.GetComponent<NetworkIdentity>();
	}
	//runs every frame
	public void Update()
	{
		
		if (!hasAuthority)
		{
			m_cableLine.SetPosition(0, gameObject.transform.position);
			if(m_connected)
				m_cableLine.SetPosition(1, m_connected.transform.TransformPoint(m_attachpoint));
			return;
		}

		if (isServer && m_id.clientAuthorityOwner == null) //prevent server default authority bug
			return;


		// if object dies while attached
		if (m_hook != null && m_hook.connectedBody == null)
		{
			if (gameObject.GetComponent<DistanceJoint2D>())
				DestroyImmediate(gameObject.GetComponent<DistanceJoint2D>());
			m_cableLine.enabled = false;
			//disable line on network
			CmdRequestDisableLine();
		}

		m_cableLine.SetPosition(0, gameObject.transform.position);
		if(m_hook != null)
			m_cableLine.SetPosition(1, m_hook.connectedBody.transform.TransformPoint(m_hook.connectedAnchor));

		//fire the cable
		if (Input.GetMouseButtonDown(1))
		{
			Vector3 l_mpos = Input.mousePosition;
			l_mpos = Camera.main.ScreenToWorldPoint(l_mpos);
			RaycastHit2D hit;
			hit = Physics2D.Raycast(transform.position, l_mpos - transform.position , Vector2.Distance(transform.position, l_mpos), Layers.ShipColLayer ^ Layers.EnemyColLayer ^ Layers.PawnColLayer);
			if (!hit)
				return;

			if (gameObject.GetComponent<DistanceJoint2D>())
				DestroyImmediate(gameObject.GetComponent<DistanceJoint2D>());

			m_hook = gameObject.AddComponent<DistanceJoint2D>();
			Rigidbody2D l_rb = hit.transform.gameObject.GetComponent<Rigidbody2D>();

			m_hook.connectedBody = l_rb;
			m_connected = l_rb.gameObject;
			m_hook.connectedAnchor = m_hook.connectedBody.transform.InverseTransformPoint(hit.point);
			m_hook.autoConfigureDistance = false;
			m_hook.enableCollision = true;
			m_hook.distance = hit.distance;
			m_hook.maxDistanceOnly = true;
			m_cableLine.enabled = true;
			m_cableLine.SetPosition(0, gameObject.transform.position);
			m_cableLine.SetPosition(1, hit.point);

			//enable line on the network
			CmdRequestEnableLine(m_connected.GetComponent<NetworkIdentity>().netId, m_hook.connectedAnchor);
		}
		//release the cable
		if (Input.GetMouseButtonUp(1))
		{
			if (gameObject.GetComponent<DistanceJoint2D>())
				DestroyImmediate(gameObject.GetComponent<DistanceJoint2D>());
			m_cableLine.enabled = false;
			//disable line on network
			CmdRequestDisableLine();
		}

	}
	#endregion

	#region Public Methods

	[Command]
	public void CmdRequestEnableLine(NetworkInstanceId p_id , Vector2 p_attachpoint)
	{
		m_connected = ClientScene.FindLocalObject(p_id);
		m_attachpoint = p_attachpoint;
		RpcEnableLine(p_id, p_attachpoint);
		m_cableLine.enabled = true;
	}

	[ClientRpc]
	public void RpcEnableLine(NetworkInstanceId p_id, Vector2 p_attachpoint)
	{
		m_connected = ClientScene.FindLocalObject(p_id);
		m_attachpoint = p_attachpoint;
		m_cableLine.enabled = true;
		m_connected.GetComponent<Rigidbody2D>().isKinematic = false;
	}

	[Command]
	public void CmdRequestDisableLine()
	{
		m_cableLine.enabled = false;
		RpcDisableLine();
	}

	[ClientRpc]
	public void RpcDisableLine()
	{
		m_cableLine.enabled = false;
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

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
	private MechaController m_mecha;
	private DistanceJoint2D m_hook;
	private LineRenderer m_cable;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{
		m_mecha = gameObject.GetComponent<MechaController>();
		m_cable = gameObject.GetComponent<LineRenderer>();

	}
	//runs every frame
	public void Update()
	{
		//request to enter/unenter an object
		if (!hasAuthority)
			return;

		
		//update the cable position stuff
		m_cable.SetPosition(0, gameObject.transform.position);
		if(m_hook != null)
			m_cable.SetPosition(1, m_hook.connectedBody.transform.TransformPoint(m_hook.connectedAnchor));
			
		
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 l_mpos = Input.mousePosition;
			l_mpos = Camera.main.ScreenToWorldPoint(l_mpos);
			RaycastHit2D hit;
			hit = Physics2D.Raycast(transform.position, l_mpos - transform.position , Vector2.Distance(transform.position, l_mpos), Layers.ShipColLayer);
			if (!hit)
				return;

			if (gameObject.GetComponent<DistanceJoint2D>())
				DestroyImmediate(gameObject.GetComponent<DistanceJoint2D>());

			m_hook = gameObject.AddComponent<DistanceJoint2D>();
			Rigidbody2D l_rb = hit.transform.gameObject.GetComponent<Rigidbody2D>();

			m_hook.connectedBody = l_rb;
			m_hook.connectedAnchor = m_hook.connectedBody.transform.InverseTransformPoint(hit.point);
			
			m_hook.autoConfigureDistance = false;
			m_hook.enableCollision = true;
			m_hook.distance = hit.distance;
			m_hook.maxDistanceOnly = true;
			m_cable.enabled = true;
			m_cable.SetPosition(0, gameObject.transform.position);
			m_cable.SetPosition(1, hit.point);
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (gameObject.GetComponent<DistanceJoint2D>())
				DestroyImmediate(gameObject.GetComponent<DistanceJoint2D>());
			m_cable.enabled = false;
		}

	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

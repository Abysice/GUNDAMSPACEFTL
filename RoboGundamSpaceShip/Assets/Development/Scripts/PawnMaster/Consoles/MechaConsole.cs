// This script is the base type for all gameobjects that the player is able to "enter"
// will be used to take network control of certain game objects
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;

public class MechaConsole : MonoBehaviour {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{

	}

	//runs every frame
	public void Update()
	{
		transform.position = transform.position;
	}


	public void OnTriggerEnter2D(Collider2D other)
	{
		//notify the player controller that they can "enter" this item
		if (other && Managers.GetInstance().GetNetworkController().isServer)
			other.gameObject.GetComponent<EnterAbility>().UpdateEnterable(Managers.GetInstance().GetPlayerManager().m_gundam);
			
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		//notify the player controller that they scan no longer "enter" this item
		if(other && Managers.GetInstance().GetNetworkController().isServer)
			other.gameObject.GetComponent<EnterAbility>().UpdateEnterable(null);
	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

// This script is the base type for all gameobjects that the player is able to "enter"
// will be used to take network control of certain game objects
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Enterable : NetworkBehaviour {

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
		//do stuff here
		Debug.Log("player " + other.gameObject.GetComponent<NetworkIdentity>().playerControllerId + " is on the trigger");
	}

	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

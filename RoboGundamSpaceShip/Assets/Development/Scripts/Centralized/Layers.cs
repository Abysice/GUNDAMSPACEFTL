// Script description goes here.
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;

public class Layers : MonoBehaviour {

	#region Public Variables
	
	//Physics Layers
	public const int PawnColLayer = 1 << 8;
	public const int PawnCol = 8;
	public const int ShipColLayer = 1 << 9;
	public const int ShipCol = 9;
	public const int MechaColLayer = 1 << 10;
	public const int MechaCol = 10;
    public const int EnemyColLayer = 1 << 11;
    public const int EnemyCol = 11;

	//sprite Layers
	public const string BackroundLayer = "Background";
	public const string SpaceDecalLayer = "SpaceDecal";
	public const string ShipLayer = "Ship";
	public const string InteriorLayer = "Interior";
	public const string PawnsLayer = "Pawns";
	public const string TurretsLayer = "Turrets";
	public const string ParticleLayer = "ParticleEffects";
	public const string MechaLayer = "Mecha";
   

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

	}
	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FlakExplosionBehaviour : NetworkBehaviour {

    #region Public Variables
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private bool AMISERVER = false;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults

    void Awake()
    {
    }
    void Start()
    {
        if (isServer)
        {
            AMISERVER = true;
            Destroy(gameObject, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        if (AMISERVER)
        {
            NetworkServer.Destroy(gameObject);
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

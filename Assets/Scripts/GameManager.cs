using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region
    public static GameManager gm;

    private void Awake()
    {
        if (!gm)
            gm = this;
        else
            Destroy(this);
    }
    #endregion

    public GameObject player; //reference to the player
    public Transform[] nodes;
    public Transform[] nodes2;
}

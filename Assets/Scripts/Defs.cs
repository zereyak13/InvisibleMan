using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defs : MonoBehaviour
{
    public static Defs Instance;
    private void Awake()
    {
        Instance = this;
    }


    //Tags
    [HideInInspector] public string playerTag = "Player";
}

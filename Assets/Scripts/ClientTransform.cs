using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;

public class ClientTransform : NetworkTransform
{
    public override void OnNetworkSpawn()
    {
        if(!IsOwner)
        {
            enabled = false;
        }
    }
}

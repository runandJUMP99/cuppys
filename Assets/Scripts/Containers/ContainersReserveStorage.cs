using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersReserveStorage : MonoBehaviour
{
    [SerializeField] Containers container;

    public Containers GetContainer()
    {
        return container;
    }
}

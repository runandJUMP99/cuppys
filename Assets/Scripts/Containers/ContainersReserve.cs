using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersReserve : MonoBehaviour
{
    [SerializeField] Containers container = Containers.Empty;

    public Containers GetContainer()
    {
        return container;
    }

    public void SetContainer(Containers setContainer)
    {
        if (container == Containers.Empty || setContainer == Containers.Empty)
        {
            container = setContainer;
        }
    }
}

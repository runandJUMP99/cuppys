using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICReserveStorage : MonoBehaviour
{
    [SerializeField] Flavors flavor;

    public Flavors GetICFlavor()
    {
        return flavor;
    }
}

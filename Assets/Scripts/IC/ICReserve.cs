using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICReserve : MonoBehaviour
{
    [SerializeField] Flavors flavor = Flavors.Empty;

    public Flavors GetICFlavor()
    {
        return flavor;
    }

    public void SetICFlavor(Flavors setFlavor)
    {
        if (flavor == Flavors.Empty || setFlavor == Flavors.Empty)
        {
            flavor = setFlavor;
        }
    }
}

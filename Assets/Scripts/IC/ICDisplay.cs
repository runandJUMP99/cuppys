using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICDisplay : MonoBehaviour
{
    [SerializeField] Flavors flavor;
    [SerializeField] int maxScoops = 6;
    int scoopsRemaining;

    void Start()
    {
        scoopsRemaining = maxScoops;
    }

    public Flavors GetICFlavor()
    {
        return flavor;
    }
    
    public void RefilICDisplay()
    {
        scoopsRemaining = maxScoops;
        Debug.Log($"{scoopsRemaining} {flavor}s remaining");
    }

    public void TakeScoop(IC ic)
    {
        if (scoopsRemaining > 0)
        {
            ic.SetICFlavor(flavor);
            scoopsRemaining--;
        }
        Debug.Log($"{scoopsRemaining} {flavor}s remaining");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingsDisplay : MonoBehaviour
{
    [SerializeField] Toppings topping;
    [SerializeField] int maxToppings = 6;
    int toppingsRemaining;

    void Start()
    {
        toppingsRemaining = maxToppings;
    }

    public Toppings GetTopping()
    {
        return topping;
    }

    public void RefilTopping()
    {
        toppingsRemaining = maxToppings;
        Debug.Log($"{toppingsRemaining} {topping}s remaining");
    }

    public void TakeScoop(IC ic)
    {
        if (toppingsRemaining > 0)
        {
            ic.AddTopping(topping);
            toppingsRemaining--;
        }
        Debug.Log($"{toppingsRemaining} {topping}s remaining");
    }
}

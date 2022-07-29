using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingsReserve : MonoBehaviour
{
    [SerializeField] Toppings topping = Toppings.Empty;

    public Toppings GetTopping()
    {
        return topping;
    }

    public void SetTopping(Toppings setTopping)
    {
        if (topping == Toppings.Empty || setTopping == Toppings.Empty)
        {
            topping = setTopping;
        }
    }
}

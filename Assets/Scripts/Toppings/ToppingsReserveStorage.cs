using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToppingsReserveStorage : MonoBehaviour
{
    [SerializeField] Toppings topping;

    public Toppings GetTopping()
    {
        return topping;
    }
}

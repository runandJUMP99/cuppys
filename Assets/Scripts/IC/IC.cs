using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IC : MonoBehaviour
{
    [SerializeField] Containers container;
    [SerializeField] Flavors flavor = Flavors.Empty;
    [SerializeField] List<Toppings> toppings = new List<Toppings>();
    [SerializeField] int maxToppings = 3;

    public Containers GetContainer()
    {
        return container;
    }

    public Flavors GetICFlavor()
    {
        return flavor;
    }

    public int GetMaxToppings()
    {
        return maxToppings;
    }

    public List<Toppings> GetToppings()
    {
        return toppings;
    }

    public void SetContainer(Containers setContainer)
    {
        if (container == Containers.Empty || setContainer == Containers.Empty)
        {
            container = setContainer;
        }
    }

    public void SetICFlavor(Flavors setFlavor)
    {
        if (flavor == Flavors.Empty || setFlavor == Flavors.Empty)
        {
            flavor = setFlavor;
        }
    }

    public void AddTopping(Toppings topping)
    {
        if (topping == Toppings.Empty)
        {
            toppings.Clear();
            return;
        }

        if (toppings.Count < maxToppings && !toppings.Contains(topping))
        {
            toppings.Add(topping);
        }
    }
}

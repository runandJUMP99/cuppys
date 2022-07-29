using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainersDisplay : MonoBehaviour
{
    [SerializeField] Containers container;
    [SerializeField] int maxContainers = 6;
    int containersRemaining;

    void Start()
    {
        containersRemaining = maxContainers;
    }

    public Containers GetContainer()
    {
        return container;
    }

    public void RefilContainers()
    {
        containersRemaining = maxContainers;
        Debug.Log($"{containersRemaining} {container}s remaining");
    }

    public void TakeContainer(IC ic)
    {
        if (containersRemaining > 0)
        {
            ic.gameObject.SetActive(true);
            ic.SetContainer(container);
            containersRemaining--;
        }
        Debug.Log($"{containersRemaining} {container}s remaining");
    }
}

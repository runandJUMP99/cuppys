using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 5f;
    CustomerSpawner customerSpawner;
    IC ic;
    ContainersReserve containerReserve;
    ICReserve icReserve;
    ToppingsReserve toppingsReserve;
    bool holdingItem = false;
    bool startedShift = false;

    void Awake()
    {
        customerSpawner = FindObjectOfType<CustomerSpawner>();
        ic = GetComponentInChildren<IC>();
        containerReserve = GetComponentInChildren<ContainersReserve>();
        icReserve = GetComponentInChildren<ICReserve>();
        toppingsReserve = GetComponentInChildren<ToppingsReserve>();
    }

    void Start()
    {
        ic.gameObject.SetActive(false);
        containerReserve.gameObject.SetActive(false);
        icReserve.gameObject.SetActive(false);
        toppingsReserve.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ProcessRaycast();
        }
    }

    void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            Clocker clocker = hit.transform.GetComponent<Clocker>();
            ContainersDisplay container = hit.transform.GetComponent<ContainersDisplay>();
            ICDisplay display = hit.transform.GetComponent<ICDisplay>();
            ToppingsDisplay topping = hit.transform.GetComponent<ToppingsDisplay>();
            ContainersReserveStorage containerReserveStorage = hit.transform.GetComponent<ContainersReserveStorage>();
            ICReserveStorage icReserveStorage = hit.transform.GetComponent<ICReserveStorage>();
            ToppingsReserveStorage toppingsReserveStorage = hit.transform.GetComponent<ToppingsReserveStorage>();
            Customer customer = hit.transform.GetComponent<Customer>();

            // if nothing of importance selected, check if item was trashed. else, return
            if (!container && !display && !topping && !containerReserveStorage && !icReserveStorage && !toppingsReserveStorage && !customer && !clocker)
            {
                if (hit.transform.name == "Trash")
                {
                   HandleTrash();
                }   
                return;
            }

            // logic to build IC
            if (container || display || topping)
            {
                HandleIC(container, display, topping);
            }
            // logic to handle reserves/storage/refils
            if (container || containerReserveStorage)
            {
                HandleContainerReserveStorage(container, containerReserveStorage);
            }
            else if (display || icReserveStorage)
            {
                HandleICReserveStorage(display, icReserveStorage);
            }
            else if (topping || toppingsReserveStorage)
            {
                HandleToppingsReserveStorage(topping, toppingsReserveStorage);
            }

            // logic for handling customers
            if (customer && ic.gameObject.activeInHierarchy)
            {
                HandleCustomer(customer);
            }

            // logic for handling Clocker
            if (clocker && !startedShift)
            {
                startedShift = true;
                customerSpawner.SpawnCustomers();
            }
        }
        else
        {
            return;
        }
    }

    void HandleContainerReserveStorage(ContainersDisplay container, ContainersReserveStorage containerReserveStorage)
    {
        if (!containerReserve.gameObject.activeInHierarchy && containerReserveStorage)
        {
            containerReserve.gameObject.SetActive(true);
            containerReserve.SetContainer(containerReserveStorage.GetContainer());
            holdingItem = true;
        }
        else if (containerReserve.gameObject.activeInHierarchy)
        {
            if (container && container.GetContainer() == containerReserve.GetContainer())
            {
                container.RefilContainers();
                containerReserve.SetContainer(Containers.Empty);
                containerReserve.gameObject.SetActive(false);
                holdingItem = false;
            }
        }
    }

    void HandleCustomer(Customer customer)
    {
        bool containerMatches = ic.GetContainer() == customer.GetContainer();
        bool flavorMatches = ic.GetICFlavor() == customer.GetICFlavor();
        List<Toppings> toppingsOnIc = ic.GetToppings();
        List<Toppings> toppingsCustomerWants = customer.GetToppings();
        bool toppingsMatch = toppingsOnIc.Count == toppingsCustomerWants.Count;
        foreach (Toppings t in toppingsOnIc)
        {
            if (!toppingsCustomerWants.Contains(t))
            {
                toppingsMatch = false;
            }
        }

        if (containerMatches && flavorMatches && toppingsMatch)
        {
            ic.SetContainer(Containers.Empty);
            ic.SetICFlavor(Flavors.Empty);
            ic.AddTopping(Toppings.Empty);
            ic.gameObject.SetActive(false);
            holdingItem = false;
            customer.CompleteCustomerOrder();
        }
    }

    void HandleIC(ContainersDisplay container, ICDisplay display, ToppingsDisplay topping)
    {
        if (container && !ic.gameObject.activeInHierarchy && !holdingItem)
        {
            container.TakeContainer(ic);
            holdingItem = true;
        }
        else if (ic.gameObject.activeInHierarchy)
        {
            if (display && ic.GetICFlavor() == Flavors.Empty)
            {
                display.TakeScoop(ic);
            }
            else if (topping && ic.GetICFlavor() != Flavors.Empty && ic.GetToppings().Count <= ic.GetMaxToppings())
            {
                if (!ic.GetToppings().Contains(topping.GetTopping()))
                {
                    topping.TakeScoop(ic);
                }
            }
        }
    }

    void HandleICReserveStorage(ICDisplay display, ICReserveStorage icReserveStorage)
    {
        if (!icReserve.gameObject.activeInHierarchy && icReserveStorage)
        {
            icReserve.gameObject.SetActive(true);
            icReserve.SetICFlavor(icReserveStorage.GetICFlavor());
            holdingItem = true;
        }
        else if (icReserve.gameObject.activeInHierarchy)
        {
            Debug.Log("HIT");
            if (display && display.GetICFlavor() == icReserve.GetICFlavor())
            {
                Debug.Log(display.GetICFlavor());
                display.RefilICDisplay();
                icReserve.SetICFlavor(Flavors.Empty);
                icReserve.gameObject.SetActive(false);
                holdingItem = false;
            }
        }
    }

    void HandleToppingsReserveStorage(ToppingsDisplay topping, ToppingsReserveStorage toppingsReserveStorage)
    {
        if (!toppingsReserve.gameObject.activeInHierarchy && toppingsReserveStorage)
        {
            toppingsReserve.gameObject.SetActive(true);
            toppingsReserve.SetTopping(toppingsReserveStorage.GetTopping());
            holdingItem = true;
        }
        else if (toppingsReserve.gameObject.activeInHierarchy)
        {
            if (topping && topping.GetTopping() == toppingsReserve.GetTopping())
            {
                topping.RefilTopping();
                toppingsReserve.SetTopping(Toppings.Empty);
                toppingsReserve.gameObject.SetActive(false);
                holdingItem = false;
            }
        }
    }

    void HandleTrash()
    {
        if (ic.gameObject.activeInHierarchy)
        {
            ic.SetContainer(Containers.Empty);
            ic.SetICFlavor(Flavors.Empty);
            ic.AddTopping(Toppings.Empty);
            ic.gameObject.SetActive(false);
        }
        if (containerReserve.gameObject.activeInHierarchy)
        {
            containerReserve.SetContainer(Containers.Empty);
            containerReserve.gameObject.SetActive(false);
        }
        if (icReserve.gameObject.activeInHierarchy)
        {
            icReserve.SetICFlavor(Flavors.Empty);
            icReserve.gameObject.SetActive(false);
        }
        if (toppingsReserve.gameObject.activeInHierarchy)
        {
            toppingsReserve.SetTopping(Toppings.Empty);
            toppingsReserve.gameObject.SetActive(false);
        }

        holdingItem = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] Targets target;

    public Targets GetTargetType()
    {
        return target;
    }
}

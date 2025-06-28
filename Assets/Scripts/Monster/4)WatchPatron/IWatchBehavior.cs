using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWatchBehavior
{
    void Watch(Transform monster, Transform player, Rigidbody2D rb);
    bool ShouldChase(Transform monster, Transform player);
    void ResetWatch();
}
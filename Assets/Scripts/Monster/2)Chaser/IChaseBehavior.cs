using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChaseBehavior
{
    void Chase(Transform monster, Transform player, Rigidbody2D rb);
}
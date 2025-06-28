using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerDetector
{
    bool IsPlayerDetected(Transform monster, Transform player);
}
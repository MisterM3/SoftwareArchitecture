using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITurretRange
{
    Collider[] GetCollidersInRadius();
    float getMaxRange();
    EventHandler<float> onRangeChanged { get; set; }
}

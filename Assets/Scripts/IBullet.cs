using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{

    Vector3 direction { get; set; }

    void Move();
    void OnHit();
}

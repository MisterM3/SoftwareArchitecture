using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable
{
    void ChangeStrategy(UpgradeStrategySO upgrade);

}


//All names of things that can be upgraded;
public enum UpgradesTags
{
    ActionSpeed,
    Damage,
    Slowness,
    Range
}

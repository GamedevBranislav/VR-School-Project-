using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureButton : RayObject
{
    public Object prefab;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        //set player mode to place furniture
        Player.instance.activeMode = InputMode.FURNITURE;

        //set active furniture prefab to this prefab
        Player.instance.activeFurniturePrefab = prefab;

    }

}

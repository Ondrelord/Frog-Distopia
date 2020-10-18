using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    void ShowHighlight();
    void HideHighlight();
    void SetState(UnitState state);
    UnitState GetState();
    GameObject GetGameObject();
}

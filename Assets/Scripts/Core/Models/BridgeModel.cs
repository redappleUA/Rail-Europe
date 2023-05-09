using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Модель, що хранить в собі всі мости, які розміщені на мапі
/// </summary>
public class BridgeModel : IModel<Bridge>
{
    public List<Bridge> Ways => new();
}

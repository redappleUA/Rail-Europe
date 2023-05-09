using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Модель, що хранить в собі всі колії, які розміщені на мапі
/// </summary>
public class RailModel : IModel<Rail>
{
    public List<Rail> Ways => new();
}

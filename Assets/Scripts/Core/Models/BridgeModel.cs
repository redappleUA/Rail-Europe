using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    /// <summary>
    /// Модель, що хранить в собі всі мости, які розміщені на мапі
    /// </summary>
    internal class BridgeModel : IModel<Bridge>
    {
        public List<Bridge> Elements { get; private set; } = new();
    }
}

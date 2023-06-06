using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    /// <summary>
    /// Модель, що хранить в собі всі колії, які розміщені на мапі
    /// </summary>
    internal class RailModel : IModel<RailView>
    {
        public List<RailView> Elements { get; private set; } = new();
    }
}

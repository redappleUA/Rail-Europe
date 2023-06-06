using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    /// <summary>
    /// ������, �� ������� � ��� �� �����, �� ������� �� ���
    /// </summary>
    internal class BridgeModel : IModel<BridgeView>
    {
        public List<BridgeView> Elements { get; private set; } = new();
    }
}

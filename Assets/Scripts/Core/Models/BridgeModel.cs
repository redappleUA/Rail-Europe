using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    /// <summary>
    /// ������, �� ������� � ��� �� �����, �� ������� �� ���
    /// </summary>
    internal class BridgeModel : IModel<Bridge>
    {
        public List<Bridge> Elements { get; private set; } = new();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    /// <summary>
    /// ������, �� ������� � ��� �� ��볿, �� ������� �� ���
    /// </summary>
    internal class RailModel : IModel<RailView>
    {
        public List<RailView> Elements { get; private set; } = new();
    }
}

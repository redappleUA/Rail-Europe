using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    /// <summary>
    /// ������, �� ������� � ��� �� ��볿, �� ������� �� ���
    /// </summary>
    internal class RailModel : IModel<Rail>
    {
        public List<Rail> Elements { get; private set; } = new();
    }
}

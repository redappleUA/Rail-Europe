using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    internal class RouteModel : IModel<Route>
    {
        /// <summary>
        /// ������ ��� ��������
        /// </summary>
        public List<Route> Elements { get; private set; } = new();
    }
}

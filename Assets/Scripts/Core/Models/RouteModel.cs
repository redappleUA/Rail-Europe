using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    internal class RouteModel : IModel<RouteScheme>
    {
        /// <summary>
        /// Список всіх маршрутів
        /// </summary>
        public List<RouteScheme> Elements { get; private set; } = new();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    internal class CityModel : IModel<CityNameReference>
    {
        public List<CityNameReference> Elements { get; private set; } = new();
    }
}

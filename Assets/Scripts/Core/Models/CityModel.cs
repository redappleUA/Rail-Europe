using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    internal class CityModel : IModel<CityView>
    {
        public List<CityView> Elements { get; private set; } = new();
    }
}

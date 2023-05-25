using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    internal class TrainModel : IModel<Train>
    {
        public List<Train> Elements { get; private set; } = new();
    }
}

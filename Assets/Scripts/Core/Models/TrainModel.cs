using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Models
{
    internal class TrainModel : IModel<TrainView>
    {
        public List<TrainView> Elements { get; private set; } = new();
    }
}

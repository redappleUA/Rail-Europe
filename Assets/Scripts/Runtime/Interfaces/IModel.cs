using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModel<T>
{
    public List<T> Ways { get; }
}

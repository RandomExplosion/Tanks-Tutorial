using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public interface IPooledObject
    {
        void OnObjectSpawn();
    } 
}

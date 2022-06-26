using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    eManagerStatus status { get; }
    void Startup();
}

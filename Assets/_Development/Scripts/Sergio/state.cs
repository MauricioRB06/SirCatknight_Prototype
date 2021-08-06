using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class state 
{ 
    // Start is called before the first frame update
   
    public Action<EnemysController> Enter;
    public Func<EnemysController,Action> LogicUpdate;
    public Action<EnemysController> Exit;
    public EnemysController EnemysController { get; private set; }
}

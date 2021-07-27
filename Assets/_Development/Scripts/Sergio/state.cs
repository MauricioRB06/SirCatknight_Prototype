using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class state 
{ 
    // Start is called before the first frame update
   
    public Action<EnemyController> Enter;
    public Func<EnemyController,Action> LogicUpdate;
    public Action<EnemyController> Exit;
    public EnemyController EnemyController { get; private set; }
}

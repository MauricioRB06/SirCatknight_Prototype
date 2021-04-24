using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected Player player;                    // Referencia a nuestro jugador
    protected PlayerStateMachine stateMachine;  // Referencia a nuestra maquina de estados
    protected PlayerData playerData;            // Aquí almacenaremos las variables de nuestro jugador

    /* el tiempo de inicio se establece cada vez que ingresamos a un estado, de esa manera desde cualquier estado
     * podemos tener una referencia de cuánto tiempo hemos estado en un estado especifico */

    protected float starTime;

    /* Cada estado va a tener una cadena que asignamos cuando creamos el estado y lo usara para decirle al animador
     * que animación deberia repoducirse */

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        /* Hacemos esto para que Unity entienda que nos estamos refiriendo a las mismas variables, ya que tienen
        * el mismo nombre */

        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;

    }

    
    public virtual void Enter() // Se llama cuando entramos en un estado especifico
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        starTime = Time.time;   // Guardaremos la hora de inicio del estado
        Debug.Log(animBoolName);
        
    }

    public virtual void Exit()  // Se llama cuando abandonamos el estado
    {
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()   // Se llama en cada Frame del juego
    {

    }

    public virtual void PhysicsUpdate() // Se llama en cada actualización fija
    {
        DoChecks();
    }

    /* La usaremos desde PhysicsUpdate() y desde Enter(), por lo que en esta función le diremos al estado si queremos
     * que busque suelo o muros, cosas así, de esa manera no tenemos que llamarlos en la actualización de física y en
     * cada estado, podemos hacerlo una vez y hacer comprobaciones  */

    public virtual void DoChecks()
    {

    }
}

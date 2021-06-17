using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/* Documentación Usada:
 * 
 * Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
 * Virtual & Override: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
 * 
 */

public class PlayerState
{
    protected Player _player;                        // Referencia a nuestro jugador
    protected PlayerStateMachine _stateMachine;      // Referencia a nuestra maquina de estados
    protected PlayerData _playerData;                // Aquí almacenaremos las variables de nuestro jugador

    protected bool _isAnimationFinished;

    protected bool _isExitingState;

    /* el tiempo de inicio se establece cada vez que ingresamos a un estado, de esa manera desde cualquier estado
     * podemos tener una referencia de cuánto tiempo hemos estado en un estado especifico */

    protected float _startTime;

    /* Cada estado va a tener un nombre que asignamos cuando creamos el estado y se usara para decirle al animador
     * que animación deberia repoducirse */

    private string _animationBoolName;  // Hace referencia a las variables dentro del animador 

    // Constructor de la clase
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animationBoolName)
    {
        this._player = player;
        this._stateMachine = stateMachine;
        this._playerData = playerData;
        this._animationBoolName = animationBoolName;
    }

    public virtual void Enter() // Se llama cuando entramos al estado
    {
        DoChecks();
        _player.playerAnimator.SetBool(_animationBoolName, true);
        _startTime = Time.time;
        _isAnimationFinished = false;
        _isExitingState = false;
    }

    public virtual void Exit()    // Se llama cuando abandonamos el estado
    {
        _player.playerAnimator.SetBool(_animationBoolName, false);
        _isExitingState = true;
    }

    public virtual void LogicUpdate()       // Se llama en cada Frame del juego
    {

    }

    public virtual void PhysicsUpdate()     // Se llama en cada actualización fija
    {
        DoChecks();
    }

    /* La usaremos desde PhysicsUpdate() y desde Enter(), por lo que en esta función le diremos al estado si queremos
     * que busque suelo o muros, cosas así, de esa manera no tenemos que llamarlos en la actualización de físicas y en
     * cada estado, podemos hacerlo una vez y hacer comprobaciones */

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => _isAnimationFinished = true;
    

}

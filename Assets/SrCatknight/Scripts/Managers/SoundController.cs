
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >

/* The Purpose Of This Script Is:

    Create a control over the life time of sound effects.

 */

using UnityEngine;

namespace SrCatknight.Scripts.Managers
{
    // Component required for this Script to work.
    [RequireComponent(typeof(AudioSource))]
    
    public class SoundController : MonoBehaviour
    {
        // Allows to destroy the Sound in a given time.
        public void Destroy(float newDestroyTime) => Destroy(gameObject, newDestroyTime);
    }
}

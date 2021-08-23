
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate an auto-tagging for the camera, which will allow me to get a reference in other scripts
//  in case someone forgets to configure it.
// 
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html

using Cinemachine;
using UnityEngine;

namespace Levels.General
{
    // Components required for this script to work.
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(CinemachineBrain))]
    
    public class MainCamera : MonoBehaviour
    {
        
        // When the camera is created, it automatically sets the tag.
        private void Awake()
        {
            gameObject.tag = "MainCamera";
        }
    }
}

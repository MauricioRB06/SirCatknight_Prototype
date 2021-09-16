
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a parallax effect for the background, which responds in relation to the camera and gives a sense of depth.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity Unnecessary Property Accesses:  https://forum.unity.com/threads/cache-transform-really-needed.356875/
//  Unity U.P.A:  https://github.com/JetBrains/resharper-unity/wiki/Avoid-multiple-unnecessary-property-accesses

using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    // Components required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    
    public class BackgroundParallax : MonoBehaviour
    {
        [Header("Camera")] [Space(5)]
        [Tooltip("Camera with respect to which parallax is to be performed")]
        [SerializeField] private GameObject parallaxCamera;
        [Space(15)]
        
        [Header("Parallax Axis")] [Space(5)]
        [Tooltip("Enables or disables the X-axis as parallax axis")] 
        [SerializeField] private bool xAxis;
        [Tooltip("Enables or disables the Y-axis as parallax axis")]
        [SerializeField] private bool yAxis;
        [Space(15)]
        
        [Header("Parallax Speed")] [Space(5)]
        [Range(0f,1f)] [Tooltip("Defines the scrolling speed of the layer")]
        [SerializeField] private float layerOffsetValue;
        
        // To store the length and initial position of the sprite used for the background.
        private float _sizeBackgroundSpriteX, _sizeBackgroundSpriteY;
        private float _startPositionBackgroundSpriteX, _startPositionBackgroundSpriteY;
        
        // To get the distance the sprite should move.
        private float _distanceToBeTraveledByTheSpriteX, _distanceToBeTraveledByTheSpriteY;
        
        // The distance traveled in relation to the parallax camera.
        private float _temporaryCameraPositionX, _temporaryCameraPositionY;
        
        // Make the initial settings to generate the parallax effect.
        private void Awake()
        {
            var backgroundSpritePosition = transform.position;
            
            // Gets the X and Y size of the background sprite.
            _sizeBackgroundSpriteX = GetComponent<SpriteRenderer>().bounds.size.x;
            _sizeBackgroundSpriteY = GetComponent<SpriteRenderer>().bounds.size.y;
            
            // Gets the initial X-axis and Y-axis position of the background sprite.
            _startPositionBackgroundSpriteX = backgroundSpritePosition.x;
            _startPositionBackgroundSpriteY = backgroundSpritePosition.y;
        }

        private void Start()
        {
            if (parallaxCamera == null)
            {
                parallaxCamera = GameObject.FindWithTag("MainCamera");
            }
        }
        
        // Based on the active parallax axes, it performs a background shift.
        private void Update()
        {
            // If X-axis parallax is activated.
            if (xAxis)
            {
                var parallaxCameraPosition = parallaxCamera.transform.position;
                
                // To save the value that the parallax camera has moved in X.
                _temporaryCameraPositionX = parallaxCameraPosition.x * (1 - layerOffsetValue);
                
                // Know the distance the sprite must travel.
                _distanceToBeTraveledByTheSpriteX = parallaxCameraPosition.x * layerOffsetValue;

                var backgroundSpriteTransform = transform;
                var newPositionBackgroundSprite = backgroundSpriteTransform.position;
                
                // Move the sprite in X.
                newPositionBackgroundSprite = new Vector3(
                    _startPositionBackgroundSpriteX + _distanceToBeTraveledByTheSpriteX,
                    newPositionBackgroundSprite.y,
                    newPositionBackgroundSprite.z);
                
                backgroundSpriteTransform.position = newPositionBackgroundSprite;

                // When the sprite travels a distance greater than its size, it moves to the right.
                if (_temporaryCameraPositionX > _startPositionBackgroundSpriteX + _sizeBackgroundSpriteX)
                {
                    _startPositionBackgroundSpriteX += _sizeBackgroundSpriteX;
                }
                
                // When the sprite travels a distance greater than its size, it moves to the left.
                if (_temporaryCameraPositionX < _startPositionBackgroundSpriteX - _sizeBackgroundSpriteX)
                {
                    _startPositionBackgroundSpriteX -= _sizeBackgroundSpriteX;
                }
            }
            else
            {
                var backgroundSpriteTransform = transform;
                var newPositionBackgroundSprite = backgroundSpriteTransform.position;
                
                // Moves the sprite in X, the same distance traveled by the parallax camera.
                newPositionBackgroundSprite = new Vector3(parallaxCamera.transform.position.x,
                    newPositionBackgroundSprite.y, newPositionBackgroundSprite.z);
                
                backgroundSpriteTransform.position = newPositionBackgroundSprite;
            }
            
            // If Y-axis parallax is activated.
            if (yAxis)
            {   
                var parallaxCameraPosition = parallaxCamera.transform.position;
                
                // To save the value that the parallax camera has moved in Y.
                _temporaryCameraPositionY = (parallaxCameraPosition.y * (1 - layerOffsetValue));
                
                // Know the distance the sprite must travel.
                _distanceToBeTraveledByTheSpriteY = parallaxCameraPosition.y * layerOffsetValue;

                var backgroundSpriteTransform = transform;
                var newPositionBackgroundSprite = backgroundSpriteTransform.position;
                
                // Move the sprite in Y.
                newPositionBackgroundSprite = new Vector3(newPositionBackgroundSprite.x,
                    _startPositionBackgroundSpriteY + _distanceToBeTraveledByTheSpriteY,
                    newPositionBackgroundSprite.z);
                
                backgroundSpriteTransform.position = newPositionBackgroundSprite;

                // When the sprite travels a distance greater than its size, it moves to the up.
                if (_temporaryCameraPositionY > _startPositionBackgroundSpriteY + _sizeBackgroundSpriteY)
                {
                    _startPositionBackgroundSpriteY += _sizeBackgroundSpriteY;
                }
                
                // When the sprite travels a distance greater than its size, it moves to the down.
                if (_temporaryCameraPositionY < _startPositionBackgroundSpriteY - _sizeBackgroundSpriteY)
                {
                    _startPositionBackgroundSpriteY -= _sizeBackgroundSpriteY;
                }
            }
            else
            {
                var backgroundSpriteTransform = transform;
                var newPositionBackgroundSprite = backgroundSpriteTransform.position;
                
                // Moves the sprite in Y, the same distance traveled by the parallax camera.
                newPositionBackgroundSprite = new Vector3(newPositionBackgroundSprite.x,
                    parallaxCamera.transform.position.y, newPositionBackgroundSprite.z);
                
                backgroundSpriteTransform.position = newPositionBackgroundSprite;
            }
        }
    }
}

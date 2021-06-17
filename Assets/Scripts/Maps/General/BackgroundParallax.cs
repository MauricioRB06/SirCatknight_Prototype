using UnityEngine;

/* Documentation:
 * 
 * Unnecessary property accesses:
 * 
 *  https://github.com/JetBrains/resharper-unity/wiki/Avoid-multiple-unnecessary-property-accesses
 *  https://forum.unity.com/threads/cache-transform-really-needed.356875/
 * 
 */

namespace Maps.General
{
    public class BackgroundParallax : MonoBehaviour
    {
        [Header("Camera")][Tooltip("Camera with respect to which parallax is to be performed")]
        // To get the camera reference
        public GameObject parallaxCamera;
        [Header("Parallax Axis")][Tooltip("Define X as the parallax axis")]
        public bool axisX;[Tooltip("Define Y as the parallax axis")]
        public bool axisY;
        [Header("Parallax Offset")]
        [Range(0f,1f)][Tooltip("Defines the layer offset factor")]
        // To control the offset of the layer
        public float offsetValue;
        
        // To get the length and starting position of the sprite
        private float _sizeBackgroundX, _sizeBackgroundY, _startPositionX, _startPositionY;
        // To get the distance the sprite should move
        private float _distanceX, _distanceY;
        // The distance traveled in relation to the camera
        private float _temporalPositionX, _temporalPositionY;

        private void Start()
        {   
            var backgroundPosition = transform.position;
            
            // We obtain the initial position of the sprite in X
            _startPositionX = backgroundPosition.x;
            // We obtain the initial position of the sprite in Y
            _startPositionY = backgroundPosition.y;
            // We obtain the size of the sprite render in X
            _sizeBackgroundX = GetComponent<SpriteRenderer>().bounds.size.x;
            // We obtain the size of the sprite render in Y
            _sizeBackgroundY = GetComponent<SpriteRenderer>().bounds.size.y;
        }

        private void Update()
        {
            if (axisX)
            {
                var positionCamera = parallaxCamera.transform.position;
                
                // To save the value that the camera has moved in X
                _temporalPositionX = (positionCamera.x * (1 - offsetValue));
                // To move the sprite, value traversed by the camera by the offset value
                _distanceX = (positionCamera.x * offsetValue);

                var backgroundTransform = transform;
                var newPosition = backgroundTransform.position;
                
                newPosition = new Vector3(_startPositionX + _distanceX, newPosition.y, newPosition.z);
                backgroundTransform.position = newPosition;

                // We move the sprite to the right when the value traversed is greater than the size of the sprite
                if (_temporalPositionX > _startPositionX + _sizeBackgroundX) _startPositionX += _sizeBackgroundX;
                // We move to the left
                else if (_temporalPositionX < _startPositionX - _sizeBackgroundX) _startPositionX -= _sizeBackgroundX;
            }
            else
            {
                var backgroundTransform = transform;
                var newPosition = backgroundTransform.position;
                
                newPosition = new Vector3(parallaxCamera.transform.position.x, newPosition.y, newPosition.z);
                backgroundTransform.position = newPosition;
            }
        
            if (axisY)
            {   
                var positionCamera = parallaxCamera.transform.position;
                
                // To save the value that the camera has moved in Y
                _temporalPositionY = (positionCamera.y * (1 - offsetValue));
                // To move the sprite, value traversed by the camera by the offset value
                _distanceY = (positionCamera.y * offsetValue);

                var backgroundTransform = transform;
                var newPosition = backgroundTransform.position;
                
                newPosition = new Vector3(newPosition.x, _startPositionY + _distanceY, newPosition.z);
                backgroundTransform.position = newPosition;

                // move the sprite up when the traverse value is larger than the sprite size
                if (_temporalPositionY > _startPositionY + _sizeBackgroundY) _startPositionY += _sizeBackgroundY;
                // we scroll down
                else if (_temporalPositionY < _startPositionY - _sizeBackgroundY) _startPositionY -= _sizeBackgroundY;
            }
            else
            {
                var backgroundTransform = transform;
                var newPosition = backgroundTransform.position;
                
                newPosition = new Vector3(newPosition.x, parallaxCamera.transform.position.y, newPosition.z);
                backgroundTransform.position = newPosition;
            }
        }
    }
}

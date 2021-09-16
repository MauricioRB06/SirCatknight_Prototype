using UnityEngine;

namespace SrCatknight.Scripts.UI
{
    public class AutoParallax : MonoBehaviour
    {
        private Material _material;
        private Vector2 _offset;

        public float xVelocity, yVelocity;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
        }

        private void Start()
        {
            _offset = new Vector2(xVelocity,yVelocity);
        }

        private void Update()
        {
            _material.mainTextureOffset += _offset * Time.deltaTime;
        }
    }
}

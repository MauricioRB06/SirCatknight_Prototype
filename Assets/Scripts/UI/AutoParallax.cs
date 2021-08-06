using UnityEngine;

namespace UI
{
    public class AutoParallax : MonoBehaviour
    {
        Material material;
        Vector2 offset;

        public float xVelocity, yVelocity;

        private void Awake()
        {
            material = GetComponent<Renderer>().material;
        }

        private void Start()
        {
            offset = new Vector2(xVelocity,yVelocity);
        }

        private void Update()
        {
            material.mainTextureOffset += offset * Time.deltaTime;
        }
    }
}

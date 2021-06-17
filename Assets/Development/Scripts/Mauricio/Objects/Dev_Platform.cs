using UnityEngine;

namespace Development.Scripts.Mauricio.Objects
{
    public class Dev_Platform : MonoBehaviour
    {

        private PlatformEffector2D _effector;
        public Collider2D _test;
        public float startWaiTime;
        private float _waitedTime;

        private void Start()
        {
            _effector = GetComponent<PlatformEffector2D>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Space))
                {
                    _waitedTime = startWaiTime;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    if (_waitedTime <= 0)
                    {
                        _effector.rotationalOffset = 180f;
                        _waitedTime = startWaiTime;
                    }
                    else
                    {
                        _waitedTime -= Time.deltaTime;
                    }
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    _effector.rotationalOffset = 0;
                }
            }
        }

        private void Update()
        {

        }
    }
}

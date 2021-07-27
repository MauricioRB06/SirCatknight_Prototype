using UnityEngine;

/* The Purpose Of This Script Is:

    Insert Here

    Documentation and References:

    Vector2.MoveTowards: https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html
    Vector2.Distance: https://docs.unity3d.com/ScriptReference/Vector2.Distance.html
    C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

 */

namespace _Development.Scripts.Mauricio.Levels.MovableObject
{
    public class MovableObject : MonoBehaviour
    {
        [Header("Movement Settings")] [Space(5)]
        [Tooltip("Sets Whether The Object Will Move or Not")]
        [SerializeField] private bool isMovableObject = true;
        [Tooltip("The Waiting Time Between Each Movement")]
        [Range(0.1F, 5F)] [SerializeField] private float movementWaitingTime = 1;
        [Tooltip("Object movement speed")]
        [Range(1F, 10F)] [SerializeField] private float movementSpeed = 1;
        [Space(15)]
        
        [Header("Platform Settings")] [Space(5)]
        [Tooltip("In Case It Is A Platform, This Option Must Be Activated")]
        [SerializeField] private bool isAPlatform;
        [Space(15)]
        
        [Header("Movement Points Route")] [Space(5)]
        [Tooltip("There must be at least 2 points on the route")]
        [SerializeField] private Transform[] movementPoints;
        [Space(15)]
        
        [Header("Damage Settings")] [Space(5)] 
        [Tooltip("If it is not a platform, it may cause damage to the player")]
        [SerializeField] private float damageToGive;
        [Space(15)]

        [Header("Audio Settings")] [Space(5)]
        [Tooltip("If Left Empty, It Will Not Reproduce Anything")]
        [SerializeField] private GameObject objectAudio;
        
        // Checks how much time has elapsed at one point before switching to the next point.
        private float _currentWaitTime;
        
        // Navigates through the movementPoints to indicate to the object to which it should move.
        private int _pointIterator;
        
        private void Start()
        {
            if (objectAudio != null)
            {
                var objectTransform = transform;
                Instantiate(objectAudio, objectTransform.position, Quaternion.identity, objectTransform);
            }
            
            _currentWaitTime = movementWaitingTime;
        }
        
        private void Update()
        {
            if (!isMovableObject) return;
            
            transform.position = Vector2.MoveTowards(transform.position, 
                movementPoints[_pointIterator].transform.position, 
                movementSpeed * Time.deltaTime);

            if (!(Vector2.Distance(transform.position, 
                movementPoints[_pointIterator].transform.position) < 0.1f)) return;
            
            if (_currentWaitTime <= 0) 
            {
                if (movementPoints[_pointIterator] != movementPoints[movementPoints.Length - 1])
                {
                    _pointIterator++;
                }
                else
                {
                    _pointIterator = 0;
                }

                _currentWaitTime = movementWaitingTime;
            }
            else
            {
                _currentWaitTime -= Time.deltaTime; 
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            
            if (isAPlatform)
            {
                collision.collider.transform.SetParent(transform);
            }
            else
            {
                collision.transform.GetComponent<Player.PlayerController>().Damage(damageToGive);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (isAPlatform)
            {
                collision.collider.transform.SetParent(null);
            }
        }

        public bool StopObject => isMovableObject = false;

        public bool MoveObject => isMovableObject = true;
    }
}

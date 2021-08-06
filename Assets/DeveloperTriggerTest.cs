
using Levels.General;
using UnityEngine;

public class DeveloperTriggerTest : MonoBehaviour
{
    public GameObject testGameObject;
    private void OnTriggerEnter2D(Collider2D other)
    {
        testGameObject.GetComponent<ExplosiveObject>().ChangeBombStatus();
    }
}

using UnityEngine;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        //private float _checkPointPositionX, _checkPointPositionY;

        private void Start()
        {
            //
            if (PlayerPrefs.GetFloat("_positionLastCheckPointX") != 0)
            {
                transform.position = (new Vector2(PlayerPrefs.GetFloat("_positionLastCheckPointX"),
                    PlayerPrefs.GetFloat("_positionLastCheckPointY")));
            }
        }
        
        // 
        public static void ReachedCheckpoint(float x, float y)
        {
            PlayerPrefs.SetFloat("_positionLastCheckPointX", x);
            PlayerPrefs.SetFloat("_positionLastCheckPointY", y);
        }
    }
}
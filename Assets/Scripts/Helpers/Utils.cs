using UnityEngine;

public class Utils : MonoBehaviour
{
    
    // returns the on-world position
   public static Vector3 ScreenToWorld( Camera camera, Vector3 position)
   {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
   }
}

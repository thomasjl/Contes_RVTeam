using UnityEngine;
using UnityEngine.SceneManagement;

public class RabbitHole : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("HeadCollider") && (!ThornSelect.instance || ThornSelect.instance.Clear))
        {
            InteractionManagerRRH.instance.LaunchNextScene();
        }
        
    }

   
}

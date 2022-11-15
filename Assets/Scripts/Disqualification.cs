using UnityEngine;

public class Disqualification : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GameManager.instance.EleminatePlayer(other.gameObject);
        }
    }
}

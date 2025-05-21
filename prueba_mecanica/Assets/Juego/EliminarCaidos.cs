using Unity.VisualScripting;
using UnityEngine;

public class EliminarCaidos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);
    }
}

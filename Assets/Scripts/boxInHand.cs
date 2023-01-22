using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxInHand : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            GameManager.Instance.Fail();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            GameManager.Instance.Fail();
        }
    }
}

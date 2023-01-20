using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int boxNumber = 2;
    public int boxIndx = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            if(other.gameObject.GetComponent<Box>().boxNumber == this.boxNumber)
            {
                if(boxNumber <= 1024)
                {
                    GameManager.Instance.BoxUpdater(other.gameObject);

                }
                else
                {
                    GameManager.Instance.BoxDestroyer(other.gameObject);
                }

                GameManager.Instance.BoxDestroyer(this.gameObject);
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            if (other.gameObject.GetComponent<Box>().boxNumber == this.boxNumber)
            {
                if (boxNumber <= 1024)
                {
                    GameManager.Instance.BoxUpdater(other.gameObject);

                }
                else
                {
                    GameManager.Instance.BoxDestroyer(other.gameObject);
                }

                GameManager.Instance.BoxDestroyer(this.gameObject);
            }
        }
    }

}

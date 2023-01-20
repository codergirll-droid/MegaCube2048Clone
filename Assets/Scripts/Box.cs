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
                GameManager.Instance.BoxDestroyer(other.gameObject);
                GetComponent<Rigidbody>().AddExplosionForce(250, this.gameObject.transform.position, 3, 3.0F);

                if (boxNumber != 2048)
                {
                    GameManager.Instance.BoxUpdater(this.gameObject);

                }
                else
                {
                    GameManager.Instance.BoxDestroyer(this.gameObject);
                }

                
            }
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int boxNumber = 2;
    public int boxIndx = 0;
    GameManager manager;

    Rigidbody rb;

    private void Start()
    {
        manager = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            if(other.gameObject.GetComponent<Box>().boxNumber == this.boxNumber)
            {
                manager.BoxDestroyer(other.gameObject);
                rb.AddExplosionForce(250, this.gameObject.transform.position, 3, 5.0F);
                manager.AddPoints();
                manager.totalPointsTxt.text = "Score " + manager.totalPoints;

                if (boxNumber != 2048)
                {
                    manager.BoxUpdater(this.gameObject);
                    manager.PopParticles(this, other.gameObject.transform.position);

                    manager.boxCount--;
                }
                else
                {
                    manager.PopParticles(this, other.gameObject.transform.position);

                    manager.BoxDestroyer(this.gameObject);
                    manager.boxCount--;
                    manager.boxCount--;

                }


            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("box"))
        {
            if (other.gameObject.GetComponent<Box>().boxNumber == this.boxNumber)
            {
                manager.BoxDestroyer(other.gameObject);
                rb.AddExplosionForce(250, this.gameObject.transform.position, 3, 5.0F);
                manager.AddPoints();
                manager.totalPointsTxt.text = "Score " + manager.totalPoints;

                if (boxNumber != 2048)
                {
                    manager.BoxUpdater(this.gameObject);
                    manager.PopParticles(this, other.gameObject.transform.position);

                    manager.boxCount--;
                }
                else
                {
                    manager.PopParticles(this, other.gameObject.transform.position);

                    manager.BoxDestroyer(this.gameObject);
                    manager.boxCount--;
                    manager.boxCount--;

                }


            }
        }
    }

}

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
                GetComponent<Rigidbody>().AddExplosionForce(250, this.gameObject.transform.position, 3, 5.0F);
                GameManager.Instance.AddPoints();
                GameManager.Instance.totalPointsTxt.text = "Score " + GameManager.Instance.totalPoints;

                if (boxNumber != 2048)
                {
                    //pop particles
                    GameManager.Instance.BoxUpdater(this.gameObject);
                    GameManager.Instance.PopParticles(gameObject.GetComponent<Box>(), other.gameObject.transform.position);

                    GameManager.Instance.boxCount--;
                }
                else
                {
                    GameManager.Instance.PopParticles(gameObject.GetComponent<Box>(), other.gameObject.transform.position);

                    GameManager.Instance.BoxDestroyer(this.gameObject);
                    GameManager.Instance.boxCount--;
                    GameManager.Instance.boxCount--;

                }


            }
        }
    }



}

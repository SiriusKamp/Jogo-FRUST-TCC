using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtkHazard : MonoBehaviour
{
    public bool recover;
    public int LifeRecovery;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Soul>() && !recover)
        {
            other.GetComponent<Soul>().gambiarra();
        }
        else if (other.GetComponent<Soul>() && recover)
        {
            other.GetComponent<Soul>().entity.currenthealth -= LifeRecovery;
            if (other.GetComponent<Soul>().entity.currenthealth <= 0)
                other.GetComponent<Soul>().entity.currenthealth = 0;
            Destroy(this.gameObject);
        }
    }

}

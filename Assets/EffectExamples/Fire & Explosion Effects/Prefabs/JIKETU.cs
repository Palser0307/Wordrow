using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JIKETU : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<ParticleSystem>().isStopped){
            Destroy(this.gameObject);
        }
    }
}

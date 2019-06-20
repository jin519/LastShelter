using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour {

    public ProgressBar Pb;
    public ProgressBarCircle PbC;

    private void Start()
    {
        Pb.BarValue = 50;
        PbC.BarValue = 50;
    }

    void FixedUpdate () {
		
        if(Input.GetKey(KeyCode.KeypadPlus))
        {
            Pb.BarValue += 1;
            PbC.BarValue += 1;
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            Pb.BarValue -= 1;
            PbC.BarValue -= 1;
        }
    }
}

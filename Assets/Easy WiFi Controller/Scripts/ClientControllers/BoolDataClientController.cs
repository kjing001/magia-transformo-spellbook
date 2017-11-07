using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using EasyWiFi.Core;

namespace EasyWiFi.ClientControls
{
    [AddComponentMenu("EasyWiFiController/Client/DataControls/Bool")]
    public class BoolDataClientController : MonoBehaviour, IClientController
    {

        public string controlName = "BoolData1";

        public GameObject controller;
        //temporary values to store information between frames
        float oldvalue = 0.0f;
        float newvalue = 0.0f;

        //value to store the average of above values.
        float averagedLoudness = 0.0f;

        //create an extra value to extra amplify our input. This way we can controll amplification per object.
        public float amp = 1.0f;

        //we reuse the backchannel data types even though this is a forward channel
        BoolBackchannelType boolData;
        string boolKey;

        //variable other script will modify via setValue to be sent across the network
        bool value;

        // Use this for initialization
        void Awake()
        {
            boolKey = EasyWiFiController.registerControl(EasyWiFiConstants.CONTROLLERTYPE_BOOL, controlName);
            boolData = (BoolBackchannelType)EasyWiFiController.controllerDataDictionary[boolKey];
        }

        //here we grab the input and map it to the data list
        void Update()
        {
            newvalue = controller.GetComponent<MicControlC>().loudness;

            //calculate the average. formula= (a+b)/c. (C equals the amount of values used, in this case 2)
            averagedLoudness = (newvalue + oldvalue) / 2;
            if (averagedLoudness > 50f)
            {
                setValue(true);
            }
            else
            {
                setValue(false);
            }

            transform.localScale = new Vector3(0.5f + averagedLoudness * amp, 0.5f + averagedLoudness * amp, 0.5f + averagedLoudness * amp);


            //save the newvalue for the use in next frame.
            oldvalue = newvalue;

            mapInputToDataStream();
        }

        public void setValue(bool b)
        {
            value = b;
        }

        public void mapInputToDataStream()
        {
            //for properties DO NOT reset to default values becasue there isn't a default
            boolData.BOOL_VALUE = value;
        }

        //this value is only menat for the UI slider, you can ignore it.
        public void sensitivity(float sense)
        {
            amp = sense;

        }


    }

}
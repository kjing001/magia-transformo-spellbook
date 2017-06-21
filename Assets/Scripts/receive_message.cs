using UnityEngine;
using System;
using System.Collections;
using EasyWiFi.Core;
using EasyWiFi.ClientBackchannels;
using UnityEngine.UI;

public class receive_message : MonoBehaviour
{
    public string currentString;
    public int currentInt;
    public float currentFloat;

    public Text showCurrentMessage;

    // Use this for initialization
    void Start()
    {

    }

    public void receiveString(StringBackchannelType stringBackchannel)
    {
        currentString = stringBackchannel.STRING_VALUE;
        

        if (currentString == "Up")
        {
            #if UNITY_ANDROID
            Handheld.Vibrate();
#endif
            showCurrentMessage.text = "Up";
        }

        if (currentString == "Left")
        {
            showCurrentMessage.text = "Left 1";
        }

        if (currentString == "Down")
        {
            showCurrentMessage.text = "Down 1";
        }

    }

  
    public void receiveInt(IntBackchannelType intBackchannel)
    {
        currentInt = intBackchannel.INT_VALUE;
        showCurrentMessage.text = currentInt.ToString() ;


        if (currentInt == 2)
        {
            #if UNITY_ANDROID
            Handheld.Vibrate();
            #endif
        }
        
    }

    public void receiveFloat(FloatBackchannelType floatBackchannel)
    {
        currentFloat = floatBackchannel.FLOAT_VALUE;
        showCurrentMessage.text = currentFloat.ToString();

        if (currentFloat == 0.3f)
        {
            #if UNITY_ANDROID
            Handheld.Vibrate();
            #endif
        }
            

    }
}

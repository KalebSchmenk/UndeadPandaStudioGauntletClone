using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour
{
    public static bool isUsingController = false;


    public void UseController()
    {
        isUsingController = !isUsingController;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapaMicroAnimacion : MonoBehaviour
{
    private PlayerLogic playerLogic;

    public void Start()
    {
        playerLogic = FindAnyObjectByType<PlayerLogic>();

    }
    //evento que se lanza desde la animacion
    public void ChangeZoom()
    {
        playerLogic.CameraNewPosition();
    }

    //referencia para devolver el control al jugador cuando termina la animacion
    public void FinishAnim()
    {
        GameManager.instance.FinishMicroscopicAnimation();
    }
}

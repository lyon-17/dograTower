using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Contains the text from the tutorial tower
public class TutorialMessagesScript : MonoBehaviour
{
    public TMP_Text text_tut;
    private int tutEvent = 0;

    // Update is called once per frame
    void Update()
    {
            if (TutorialScript.tutorial.getPlayerPositionX() == 8 && TutorialScript.tutorial.getPlayerPositionY() > 8 && TutorialScript.tutorial.getPlayerPositionY() < 14)
            {
                text_tut.text = "Estas escaleras son para salir del tutorial. Puedes usarlas en cualquier momento";
                return;
            }
            if (tutEvent == 0)
                text_tut.text = "Bienvenido al tutorial. Para empezar, te puedes mover con las teclas de direccion";
            if ((TutorialScript.tutorial.getPlayerPositionX() != 8 || TutorialScript.tutorial.getPlayerPositionY() != 7) && tutEvent == 0)
                tutEvent = 1;
            if (tutEvent == 1)
                text_tut.text = "Ya sabes moverte. Durante el juego te encontraras multiples puertas cerradas, necesitaras llaves para abrirlas";
            if (TutorialScript.tutorial.playerYellowKeys > 0 && tutEvent == 1)
                tutEvent = 2;
            if (tutEvent == 2)
                text_tut.text = "Has cogido una llave, con ella puedes abrir una puerta, pero solo si es del mismo color.";
            if (TutorialScript.tutorial.playerYellowKeys == 0 && tutEvent == 2)
                tutEvent = 3;
            if (tutEvent == 3)
                text_tut.text = "La siguiente llave esta detras de un enemigo. Para luchar con el solo tienes que entrar en su rango";
            if (tutEvent == 3 && TutorialScript.tutorial.playerHealth == 97)
                tutEvent = 4;
            if (tutEvent == 4)
                text_tut.text = "Durante tu aventura te encontraras pociones que te restauran una determinada cantidad de vida y gemas que incrementan tus capacidades";
            if (TutorialScript.tutorial.playerGreenKeys == 1 && tutEvent == 4)
                tutEvent = 5;
            if (tutEvent == 5)
                text_tut.text = "Es recomendable obtener mas estadisticas cuando antes para perder la menor cantidad de vida posible.";
            if (tutEvent == 5 && TutorialScript.tutorial.playerYellowKeys == 1)
                tutEvent = 6;
            if (tutEvent == 6)
                text_tut.text = "En muchisimas ocasiones tendras varias opciones y meditar que pasos debes seguir. En este caso derrota al monstruo y no uses la llave que acabas de conseguir";
            if (tutEvent == 6 && TutorialScript.tutorial.getPlayerPositionX() == 10 && TutorialScript.tutorial.getPlayerPositionY() == 4)
                text_tut.text = "Decidiste ignorarme y ahora no puedes continuar. Tu unica opcion es reiniciar o salir del tutorial.";
            if (tutEvent == 6 && TutorialScript.tutorial.getPlayerPositionX() == 13 && TutorialScript.tutorial.getPlayerPositionY() == 0)
                tutEvent = 7;
            if (tutEvent == 7)
                text_tut.text = "Posiblemente te hayas dado cuenta, pero puedes ver las estadisticas de los monstruos y la vida que te quitan si pones el raton encima de ellos";
            if (TutorialScript.tutorial.getPlayerPositionX() == 5 && TutorialScript.tutorial.getPlayerPositionY() == 11 && tutEvent == 7)
                tutEvent = 8;
            if(tutEvent == 8)
                text_tut.text = "Has completado la primera planta, las escaleras suben y bajan un piso de forma automatica. Recuerda que puedes usarlas todas las veces que quieras";
            if (TutorialScript.tutorial.getPlayerPositionY() > 19)
                tutEvent = 9;
            if (tutEvent == 9)
                text_tut.text = "Con lo aprendido intenta obtener la pocion grande. Con ello el tutorial se da por terminado.";
    }
}

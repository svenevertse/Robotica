using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyStats : MonoBehaviour {

    public float increasePercentage;

    public int [] damage;
    public float [] speed;
    public int [] points;
    public int [] health;
    public int [] max;

    public void CalulateDifficulty ()                               //functie die de statistieken set van de enemies
    {

        for (int i = 0; i < damage.Length; i++)                     //for loop die alle variablen in de array bijlangs gaat 
        {

            if (!(damage[i] > max[0]))                              //conditie die checked of de waarde niet hoger in dan de maximale waarde
            {

                float damageF = (float)damage[i];                   //local variable die de gecaste waarde van de int pakt

                damageF += damageF / 100f * increasePercentage;     //formule de waarde van de statistiek exponentieel verhoogt
                damage[i] = Mathf.RoundToInt(damageF);              //set de waarde zodat de enemy hem kan overnemen als de wave start

            }                                                       //bovenstaande geld ook voor de andere for loops in deze class
        }      

        for (int i = 0; i < speed.Length; i++)                      
        {

            if (!(speed[i] > (float)max[1]))                        
            {

                float speedF = speed[i];                           

                speedF += speedF / 100f * increasePercentage;       
                speed[i] = speedF;                                  

            }
        }

        for (int i = 0; i < health.Length; i++)
        {

            if(!(health[i] > max[2]))
            {

                float healthF = (float)health[i];

                healthF += healthF / 100f * increasePercentage;
                health[i] = Mathf.RoundToInt(healthF);

            }
        }

    }

}

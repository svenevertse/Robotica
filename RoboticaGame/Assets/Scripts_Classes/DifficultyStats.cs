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

    public void CalulateDifficulty ()
    {

        for (int i = 0; i < damage.Length; i++)
        {

            if (!(damage[i] > max[0]))
            {

                float damageF = (float)damage[i];

                damageF += damageF / 100f * increasePercentage;
                damage[i] = Mathf.RoundToInt(damageF);

            }
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

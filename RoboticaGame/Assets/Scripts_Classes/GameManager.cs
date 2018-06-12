using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class die de meeste belangrijke gegevens van het spel bijhoud en managed.
/// Deze class houd bij hoeveel enemies er in de wave zijn, de score van de speler, de highscore van de speler en de moeilijkheidgraad van het spel
/// Ook update deze class de Highscore.
/// </summary>
public class GameManager : MonoBehaviour
{

    public int currentPoints;
    public int currentAmountEnemies;

    public Image img;                                                       //zwarte afbeelding die langzaam verdwijnt aan het begin van het spel
    public Text text;                                                       //text met instructies voor de speler

    DifficultyStats difficultyStats;

    public enum Difficulty                                                  //enum voor de moeilijkheidsgraad van het spel
    {

        Recruit = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3,
        Veteran = 4

    }

    public Difficulty enemyDifficulty;

    public static GameManager ins;

    void Awake ()
    {

        ins = this;

    }

    /// <summary>
    /// De start functie die alles in het spel initialiseerd. 
    /// Ook laad hij de Highscore tijdens deze start functie en laat deze functie de speler pas bewegen op het moment dat de coroutine afgelopen is.
    /// </summary>
    void Start()
    {

        difficultyStats = GetComponent <DifficultyStats>();

        StartCoroutine(FadeImage(true));
        StartCoroutine(FadeText(10f));

        MainCharacterController.ins.LockCursor(true);
        MainCharacterController.ins.enabled = false;
        StartCoroutine(StartPlayerMovement(1.5f));

        Time.timeScale = 1;

        enemyDifficulty = Difficulty.Recruit;

        XmlManager.ins.Load();
        UI_Controller.ins.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

    }

    /// <summary>
    /// Functie die de punten opslaat en bijhoud die de speler verdient.
    /// Ook checked deze functie of de Highscore verbroken is.
    /// </summary>
    public void GetPoints(int givenPoints)
    {

        currentPoints += givenPoints;
        UI_Controller.ins.UpdatePoints(currentPoints);

        if (currentPoints > XmlManager.ins.newHighscore.highscore)
        {

            UpdateHighscore();

        }

    }

    /// <summary>
    /// Functie die de highscore update en opslaat in het XML bestand
    /// </summary>
    public void UpdateHighscore()
    {

        XmlManager.ins.newHighscore.highscore = currentPoints;
        XmlManager.ins.Save();
        UI_Controller.ins.UpdateHighscoreText(XmlManager.ins.newHighscore.highscore);

    }

    /// <summary>
    /// Functie die telkens als er een enemy uitgeschakeld word update en checked hoeveel enemies er nog beschikbaar zijn voor deze wave.
    /// Ook checked deze functie of er geen enemies meer zijn voor deze wave en activeerd dan de nieuwe wave.
    /// </summary>
    public void EraseEnemy()
    {

        currentAmountEnemies--;
        WaveBasedSystem.ins.curInLevel--;

        if (currentAmountEnemies < 1)
        {

            SoundSystem.ins.PlayAudio(SoundSystem.SoundState.WaveEnding);
            difficultyStats.CalulateDifficulty();
            WaveBasedSystem.ins.CalculateEnemyAmount(WaveBasedSystem.ins.enemyAmount);
            

        }

    }

    /// <summary>
    /// Functie die tijdens de start van een nieuwe wave enemies aangeeft hoeveel enemies er deze wave zijn
    /// </summary>
    public void UpdateEnemyAmount(int enemyAmount)
    {

        currentAmountEnemies = enemyAmount;

    }

    /// <summary>
    /// Coroutine die ervoor zorgt dat je een Image Fade effect krijgt. 
    /// Daarmee bedoel ik dat : Op het moment dat het spel start heb je een zwart scherm wat langzaam verdwijnt.
    /// </summary>
    IEnumerator FadeImage(bool fadeAway)
    {

        if (fadeAway == true)
        {

            for (float i = 1; i >= 0; i -= Time.deltaTime)                                          //for loop dat er voor zorgt dat je het effect krijgt dat het zwarte scherm ook langzaam verdwijnt.
            {

                img.color = new Color(0, 0, 0, i);
                yield return null;

            }
        }

        yield return new WaitForSeconds(2f);
        img.color = new Color(0, 0, 0, 0);                                                          //zorgt ervoor dat het zwarte scherm ook helemaal weg is aan het eind van deze coroutine

    }

    /// <summary>
    /// Coroutine die ervoor zorgt dat de text met instructies na verloop van tijd verdwijnt.
    /// </summary>
    IEnumerator FadeText(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

            text.color = new Color(1, 0, 0, 0);

    }

    /// <summary>
    /// Coroutine die ervoor zorgt dat de speler pas kan bewegen na een paar seconden.
    /// Hier is voor gekozen zodat tijdens dat het zwarte scherm uitfade de speler niet kan lopen en het level goed kan laden.
    /// </summary>
    IEnumerator StartPlayerMovement (float time)
    {

        yield return new WaitForSeconds(time);

        MainCharacterController.ins.enabled = true;

    }
}

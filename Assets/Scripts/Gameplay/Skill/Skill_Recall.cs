using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Recall : SkillBase
{
    [Header("Lab Recall")]
    public float enemyDistanceRadius;

    public override void SkillUse()
    {
        if (GameManager.scriptPlayer.SpendEnergy(energyCost))
        {
            List<Collider2D> collidersNearby = new List<Collider2D>();
            collidersNearby.AddRange(Physics2D.OverlapCircleAll(GameManager.scriptPlayer.gameObject.transform.position,enemyDistanceRadius));
            
            bool enemiesNearby = false;

            foreach (var item in collidersNearby)
            {
                if(item.gameObject.GetComponent<CreatureAtributes>())
                {
                    if (item.gameObject.GetComponent<CreatureAtributes>().creatureFaction == Faction.Virus)
                    {
                        //Debug.Log(item.gameObject.name);

                        enemiesNearby = true;
                    }
                }

            }

            if(enemiesNearby)
            {
                StopCoroutine(CantRecall());
                StartCoroutine(CantRecall());
            }
            else
                Activate();
        }
    }

    public override void Activate()
    {
        base.Activate();

        RecallWindowOpenClose(true);

    }

    public IEnumerator CantRecall()
    {
        GameManager.scriptCanvas.recallUnable.SetBool("active", true);
        yield return new WaitForSeconds(1);
        GameManager.scriptCanvas.recallUnable.SetBool("active", false);

    }

    public void RecallWindowOpenClose(bool open)
    {
        GameManager.scriptCanvas.recallConfirmation.SetBool("active", open);

        if(open)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

}

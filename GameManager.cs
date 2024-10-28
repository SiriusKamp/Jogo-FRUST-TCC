using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //batalha
    public Int32 CalculateHealth(Entity entity)
    {
        // Formula: (resistence * 10) + (level * 4) + 10
        Int32 result = (entity.resistence * 10) + (entity.lvl * 4);
        float temp;
        temp = result * entity.buffHealth;
        result = (int)Math.Round(temp);
        Debug.LogFormat("CalculateHealth: {0}", result);
        return result;
    }
    public Int32 Calculatefurt(Entity entity)
    {
        Int32 result = (entity.furt * 10) + (entity.lvl * 10);
        float temp;
        temp = result * entity.buffFurt;
        result = (int)Math.Round(temp);
        Debug.LogFormat("Calculatefurt: {0}", result);
        return result;
    }
    public Int32 Calculatestamina(Entity entity)
    {
        Int32 result = entity.stamina + (entity.lvl * 5);
        float temp;
        temp = result * entity.buffStm;
        result = (int)Math.Round(temp);
        Debug.LogFormat("Calculatestamina: {0}", result);
        return result;
    }
    public Int32 Calculatedamage(Entity entity)
    {
        Int32 result = (entity.will * 6) + (entity.lvl * 6);
        float temp;
        temp = result * entity.buffdmg;
        result = (int)Math.Round(temp);

        Debug.LogFormat("Calculatedamage: {0}", result);
        return result;
    }
    public Int32 Calculatedefense(Entity entity)
    {
        System.Random rnd = new System.Random();
        Int32 result = (entity.resistence * 2) + (entity.lvl * 3) + ((entity.CapRes + entity.ColarRes + entity.AccRes + entity.GloveRes + entity.BootsRes) * 2);
        float temp;
        temp = result * entity.buffRes;
        result = (int)Math.Round(temp);

        Debug.LogFormat("Calculatedefence: {0}", result);
        return result;
    }
    public Int32 CalculateLevel(Entity entity)
    {
        int result = entity.lvl;
        // Formula: lvl * 100 = xp para upar
        if (entity.lvl * 100 <= entity.xp && entity.wasadd <= entity.lvl)
        {
            float percent = entity.currenthealth / entity.maxhealth;
            result = entity.lvl + 1;
            entity.will += entity.will / 2 + 1;
            entity.resistence += entity.resistence / 2 + 1;
            entity.stamina += entity.stamina / 2;
            entity.furt += entity.furt / 2;
            entity.perseption += entity.perseption / 2;
            entity.recoverystm++;
            entity.xp = entity.xp - (entity.lvl * 100);
            entity.lvl += 1;
            entity.maxhealth = CalculateHealth(entity);
            entity.currenthealth = entity.maxhealth * percent;
            entity.maxstamina = Calculatestamina(entity);
            entity.maxstealth = Calculatefurt(entity);
        }
        else if (entity.lvl * 100 <= entity.xp && entity.wasadd >= entity.lvl)
        {
            float percent = entity.currenthealth / entity.maxhealth;

            result = entity.lvl + 1;
            float will = entity.will / 0.8f;
            entity.will = (int)Math.Round(will);
            float resistence = entity.resistence / 0.8f;
            entity.resistence = (int)Math.Round(resistence);
            float stamina = entity.stamina / 0.8f;
            entity.stamina = (int)Math.Round(stamina);
            float furt = entity.furt / 0.8f;
            entity.furt = (int)Math.Round(furt);
            float perseption = entity.perseption / 0.8f;
            entity.perseption = (int)Math.Round(perseption);
            entity.xp = entity.xp - entity.lvl * 100;
            entity.lvl += 1;
            entity.maxhealth = CalculateHealth(entity);
            entity.currenthealth = entity.maxhealth * percent;
            entity.maxstamina = Calculatestamina(entity);
            entity.maxstealth = Calculatefurt(entity);
            entity.currentstamina = Calculatestamina(entity);
        }
        else if (entity.xp < 0 && entity.lvl > 1)
        {
            float percent = entity.currenthealth / entity.maxhealth;
            float xpp = entity.xp + (entity.lvl * 100);
            float xpercent = xpp / (entity.lvl * 100);

            result = entity.lvl + 1;
            float will = entity.will * 0.8f;
            entity.will = (int)Math.Round(will);
            float resistence = entity.resistence * 0.8f;
            entity.resistence = (int)Math.Round(resistence);
            float stamina = entity.stamina * 0.8f;
            entity.stamina = (int)Math.Round(stamina);
            float furt = entity.furt * 0.8f;
            entity.furt = (int)Math.Round(furt);
            float perseption = entity.perseption * 0.8f;
            entity.perseption = (int)Math.Round(perseption);
            entity.lvl -= 1;
            float xp = entity.lvl * 100 * xpercent;
            entity.xp = (int)Math.Round(xp); ;
            entity.maxhealth = CalculateHealth(entity);
            entity.currenthealth = entity.maxhealth * percent;
            entity.maxstamina = Calculatestamina(entity);
            entity.maxstealth = Calculatefurt(entity);
            entity.currentstamina = Calculatestamina(entity);

        }
        else if (entity.xp < 0 && entity.lvl == 1)
            entity.xp = 0;
        Int32 Result = result;
        Debug.LogFormat("level: {0}", Result);
        return Result;

    }

}


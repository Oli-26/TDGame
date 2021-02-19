using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects
{
    List<Effect> effects = new List<Effect>();
    public bool Stunned = false;
    public bool Slowed = false;
    public float SlowPercent = 0f;
    public bool AbilityBlocked = false;
    public bool StunImmune = false;
    public bool StunBonusDamage = false;
    public float StunBonusDamageMultiplier = 1f;

    public void AddEffect(EffectType type, float time, float value){ 
        effects.Add(new Effect(type, time, value));
        UpdateFlags();
    }
    public void AddEffect(EffectType type, float time){ 
        effects.Add(new Effect(type, time));
        UpdateFlags();
    }

    public void ReduceEffects(float time){
        bool reduced = false;
        foreach(var effect in effects.ToArray()){
            effect.timeLeft-=time;
            if(effect.timeLeft < 0){
                effects.Remove(effect);
                reduced = true;
            }
        }
        if(reduced){
            UpdateFlags();
        }
    }

    void RefreshSlow(){

    }

    public void UpdateFlags(){
        Slowed = false;
        Stunned = false;
        AbilityBlocked = false;
        StunImmune = false;
        SlowPercent = 0;
        StunBonusDamage = false;
        StunBonusDamageMultiplier = 1f;


        foreach(var effect in effects){
            switch(effect.type){
                case EffectType.SLOW:
                        Slowed = true;
                        if(effect.value > SlowPercent)
                            SlowPercent = effect.value;
                        
                        break;
                case EffectType.STUN:
                        Stunned = true;
                        break;
                case EffectType.ABILITYBLOCK:
                        AbilityBlocked = true;
                        break;
                case EffectType.STUNRESIST:
                        StunImmune = true;
                        break;
                case EffectType.STUNBONUS:
                        StunBonusDamage = true;
                        if(effect.value > StunBonusDamageMultiplier)
                            StunBonusDamageMultiplier = effect.value;
                        break;
                default:
                        break;
                        
            }
        }
    }
}

public class Effect
{
    public EffectType type;
    public float timeLeft = 0f;
    public float value = 0f;

    public Effect(EffectType type, float time){
        this.type = type;
        this.timeLeft = time;
        this.value = 0f;
    }
    public Effect(EffectType type, float time, float value){
        this.type = type;
        this.timeLeft = time;
        this.value = value;
    }
}

public enum EffectType {SLOW, STUN, ABILITYBLOCK, STUNRESIST, STUNBONUS}
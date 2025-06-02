﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class FadeEffect
{
    public static IEnumerator Fade(SpriteRenderer target, float start, float end, float fadeTime=1f, UnityAction action=null)
    {
        if ( target == null ) yield break;
        
        float percent = 0;
        
        while ( percent < 1 )
        {
        	percent += Time.deltaTime / fadeTime;
        
            Color color  = target.color;
            color.a      = Mathf.Lerp(start, end, percent);
            target.color = color;
        
            yield return null;
        }
        
        action?.Invoke();
    }
}


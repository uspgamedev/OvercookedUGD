using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public static class GameEasings
{
    public static Ease OrderCardFadeEase => Ease.InCubic;
    public static float OrderCardFadeDuration => 1f;

    public static Vector3 FinalTablePunchVector => new Vector3(-10f, 0f, 0f);
    public static float FinalTablePunchDuration => 2f;

    public static Ease FinalTableFadeEase => Ease.InOutExpo;
    public static float FinalTableFadeDuration => 1f;
}

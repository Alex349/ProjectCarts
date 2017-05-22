using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(IA_Item))]
public class IA_Item_Inspector : Editor
{
    private bool showRocketItem = false, showBananaItem = false, showTurboItem = false, showFrozeItem = false, showSpawners = false, showGeneral = false;

    private SerializedObject m_theScript;

    private SerializedProperty currentIAItem, money, myPosition, iADefaultSpeed, iADefaultAcc;
    private SerializedProperty IaUseItemCooldown, startRaceCooldown;
    private SerializedProperty rocketEffect, rocketSpeed, rocketAcc, rocketEffectDuration;
    private SerializedProperty bananaEffect, bananaEffectDuration, bananaSlowedSpeed, bananaSlowedAcc;
    private SerializedProperty turboEffect, turboSpeed, turboAcc, turboEffectDuration;
    private SerializedProperty frozeEffect, frozeSpeed, frozeAcc, frozeEffectDuration;
    private SerializedProperty backSpawn, backSpawnMiddle, backSpawnLast, frontSpawn;



    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        m_theScript.ApplyModifiedProperties();
        m_theScript.UpdateIfRequiredOrScript();

        showGeneral = EditorGUILayout.Foldout(showGeneral, "General Settings");

        if (showGeneral)
        {
            EditorGUILayout.PropertyField(currentIAItem);
            EditorGUILayout.PropertyField(money);
            EditorGUILayout.PropertyField(myPosition);

            EditorGUILayout.PropertyField(iADefaultSpeed);
            EditorGUILayout.PropertyField(iADefaultAcc);

            EditorGUILayout.PropertyField(IaUseItemCooldown);
            EditorGUILayout.PropertyField(startRaceCooldown);
        }

        showRocketItem = EditorGUILayout.Foldout(showRocketItem, "Rocket Settings");

        if (showRocketItem)
        {
            EditorGUILayout.PropertyField(rocketEffect);
            EditorGUILayout.PropertyField(rocketSpeed);
            EditorGUILayout.PropertyField(rocketAcc);
            EditorGUILayout.PropertyField(rocketEffectDuration);
        }

        showBananaItem = EditorGUILayout.Foldout(showBananaItem, "Banana Settings");

        if (showBananaItem)
        {
            EditorGUILayout.PropertyField(bananaEffect);
            EditorGUILayout.PropertyField(bananaSlowedSpeed);
            EditorGUILayout.PropertyField(bananaSlowedAcc);
            EditorGUILayout.PropertyField(bananaEffectDuration);

        }

        showTurboItem = EditorGUILayout.Foldout(showTurboItem, "Turbo Settings");

        if (showTurboItem)
        {
            EditorGUILayout.PropertyField(turboEffect);
            EditorGUILayout.PropertyField(turboSpeed);
            EditorGUILayout.PropertyField(turboAcc);
            EditorGUILayout.PropertyField(turboEffectDuration);
        }
        showFrozeItem = EditorGUILayout.Foldout(showFrozeItem, "Froze Settings");

        if (showFrozeItem)
        {
            EditorGUILayout.PropertyField(frozeEffect);
            EditorGUILayout.PropertyField(frozeSpeed);
            EditorGUILayout.PropertyField(frozeAcc);
            EditorGUILayout.PropertyField(frozeEffectDuration);
        }

        showSpawners = EditorGUILayout.Foldout(showSpawners, "Spawners Settings");

        if (showSpawners)
        {
            EditorGUILayout.PropertyField(backSpawn);
            EditorGUILayout.PropertyField(backSpawnMiddle);
            EditorGUILayout.PropertyField(backSpawnLast);
            EditorGUILayout.PropertyField(frontSpawn);
        }

    }

    public void OnEnable()
    {

        m_theScript = new SerializedObject(target);

        currentIAItem = m_theScript.FindProperty("currentIAItem");
        money = m_theScript.FindProperty("money");
        myPosition = m_theScript.FindProperty("myPosition");

        iADefaultSpeed = m_theScript.FindProperty("iADefaultSpeed");
        iADefaultAcc = m_theScript.FindProperty("iADefaultAcc");

        IaUseItemCooldown = m_theScript.FindProperty("IaUseItemCooldown");
        startRaceCooldown = m_theScript.FindProperty("startRaceCooldown");

        rocketEffect = m_theScript.FindProperty("rocketEffect");
        rocketSpeed = m_theScript.FindProperty("rocketSpeed");
        rocketAcc = m_theScript.FindProperty("rocketAcc");
        rocketEffectDuration = m_theScript.FindProperty("rocketEffectDuration");

        bananaEffect = m_theScript.FindProperty("bananaEffect");
        bananaSlowedSpeed = m_theScript.FindProperty("bananaSlowedSpeed");
        bananaSlowedAcc = m_theScript.FindProperty("bananaSlowedAcc");
        bananaEffectDuration = m_theScript.FindProperty("bananaEffectDuration");

        turboEffect = m_theScript.FindProperty("turboEffect");
        turboSpeed = m_theScript.FindProperty("turboSpeed");
        turboAcc = m_theScript.FindProperty("turboAcc");
        turboEffectDuration = m_theScript.FindProperty("turboEffectDuration");

        frozeEffect = m_theScript.FindProperty("frozeEffect");
        frozeSpeed = m_theScript.FindProperty("frozeSpeed");
        frozeAcc = m_theScript.FindProperty("frozeAcc");
        frozeEffectDuration = m_theScript.FindProperty("frozeEffectDuration");

        backSpawn = m_theScript.FindProperty("backSpawn");
        backSpawnMiddle = m_theScript.FindProperty("backSpawnMiddle");
        backSpawnLast = m_theScript.FindProperty("backSpawnLast");
        frontSpawn = m_theScript.FindProperty("frontSpawn");

    }

}


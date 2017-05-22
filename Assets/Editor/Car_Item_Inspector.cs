using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(m_carItem))]
public class Car_Item_Inspector : Editor {

    private bool showRocketItem = false, showBananaItem = false, showTurboItem = false, showFrozeItem = false, showSpawners = false, showGeneral = false;

    private SerializedObject m_theCarScript;

    private SerializedProperty currentPlayerObject, money, myPosition, carDefaultSpeed, carDefaultAcc;
    private SerializedProperty rocketEffect, rocketSpeed, rocketAcc, rocketEffectDuration;
    private SerializedProperty bananaEffect, bananaEffectDuration, bananaSlowedSpeed, bananaSlowedAcc;
    private SerializedProperty turboEffect, turboSpeed, turboAcc, turboEffectDuration;
    private SerializedProperty frozeEffect, frozeSpeed, frozeAcc, frozeEffectDuration;
    private SerializedProperty backSpawn, backSpawnMiddle, backSpawnLast, frontSpawn;



    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        m_theCarScript.ApplyModifiedProperties();
        m_theCarScript.UpdateIfRequiredOrScript();

        showGeneral = EditorGUILayout.Foldout(showGeneral, "General Settings");

        if (showGeneral)
        {
            EditorGUILayout.PropertyField(currentPlayerObject);
            EditorGUILayout.PropertyField(money);
            EditorGUILayout.PropertyField(myPosition);

            EditorGUILayout.PropertyField(carDefaultSpeed);
            EditorGUILayout.PropertyField(carDefaultAcc);

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

        m_theCarScript = new SerializedObject(target);

        currentPlayerObject = m_theCarScript.FindProperty("currentPlayerObject");
        money = m_theCarScript.FindProperty("money");
        myPosition = m_theCarScript.FindProperty("myPosition");

        carDefaultSpeed = m_theCarScript.FindProperty("carDefaultSpeed");
        carDefaultAcc = m_theCarScript.FindProperty("carDefaultAcc");

        rocketEffect = m_theCarScript.FindProperty("rocketEffect");
        rocketSpeed = m_theCarScript.FindProperty("rocketSpeed");
        rocketAcc = m_theCarScript.FindProperty("rocketAcc");
        rocketEffectDuration = m_theCarScript.FindProperty("rocketEffectDuration");

        bananaEffect = m_theCarScript.FindProperty("bananaEffect");
        bananaSlowedSpeed = m_theCarScript.FindProperty("bananaSlowedSpeed");
        bananaSlowedAcc = m_theCarScript.FindProperty("bananaSlowedAcc");
        bananaEffectDuration = m_theCarScript.FindProperty("bananaEffectDuration");

        turboEffect = m_theCarScript.FindProperty("turboEffect");
        turboSpeed = m_theCarScript.FindProperty("turboSpeed");
        turboAcc = m_theCarScript.FindProperty("turboAcc");
        turboEffectDuration = m_theCarScript.FindProperty("turboEffectDuration");

        frozeEffect = m_theCarScript.FindProperty("frozeEffect");
        frozeSpeed = m_theCarScript.FindProperty("frozeSpeed");
        frozeAcc = m_theCarScript.FindProperty("frozeAcc");
        frozeEffectDuration = m_theCarScript.FindProperty("frozeEffectDuration");

        backSpawn = m_theCarScript.FindProperty("backSpawn");
        backSpawnMiddle = m_theCarScript.FindProperty("backSpawnMiddle");
        backSpawnLast = m_theCarScript.FindProperty("backSpawnLast");
        frontSpawn = m_theCarScript.FindProperty("frontSpawn");

    }
}

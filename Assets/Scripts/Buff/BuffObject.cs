using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class BuffObject : MonoBehaviour
{
    [SerializeField]private BuffType type;
    [SerializeField]private float value;
    [SerializeField]private float time;
    [SerializeField]private bool isTimeBuff;

    public BuffType Type { get => type; set => type = value; }
    public float Value { get => value; set => this.value = value; }
    public float Time { get => time; set => time = value; }
    public bool IsTimeBuff { get => isTimeBuff; set => isTimeBuff = value; }
}


//[CanEditMultipleObjects]
//[CustomEditor(typeof(BuffObject))]
//public class BuffObjectEditor : Editor {

//    private enum TimeDrop { ��,���}
//    public override void OnInspectorGUI()
//    {
//        BuffObject t = target as BuffObject;

//        GUILayout.BeginHorizontal();
//        GUILayout.Label("��� :");
//        t.Type = (BuffType)EditorGUILayout.EnumPopup(t.Type);
//        GUILayout.EndHorizontal();
//        GUILayout.Space(15);
//        EditorGUILayout.BeginFoldoutHeaderGroup(true, "��������� �������");
        
//        if (t.Type == BuffType.SpeedIncrease) {
//            GUILayout.BeginHorizontal();
//            GUILayout.Label("�������� :");
//            t.Value = EditorGUILayout.FloatField(t.Value);
//            GUILayout.EndHorizontal();

//            GUILayout.Space(7);
//        }

//        GUILayout.BeginHorizontal();
//        GUILayout.Label("������ ����������� �� �����?  ");
//        t.IsTimeBuff = ((TimeDrop)EditorGUILayout.EnumPopup(t.IsTimeBuff ? TimeDrop.�� : TimeDrop.���) == TimeDrop.��) ? true : false;
//        GUILayout.EndHorizontal();


//        if (t.IsTimeBuff) {
//            GUILayout.Space(7);

//            GUILayout.BeginHorizontal();
//            GUILayout.Label("����� �������(�������):");
//            t.Time = EditorGUILayout.Slider(t.Time, 0f, 60f);
//            GUILayout.EndHorizontal();
//        }

//        EditorGUILayout.EndFoldoutHeaderGroup();



//        EditorUtility.SetDirty(t);
//    }
//}

public enum BuffType { 
    JumpBlock,
    MoveReverse,
    SpeedIncrease
}

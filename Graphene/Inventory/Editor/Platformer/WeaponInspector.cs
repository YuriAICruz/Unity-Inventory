﻿using System.Collections;
using System.Collections.Generic;
using Graphene.Inventory.Wearables;
using UnityEditor;
using UnityEngine;

namespace Graphene.Inventory
{
    [CustomEditor(typeof(WeaponPlatformer), true)]
    public class WeaponInspector : Editor
    {
        private WeaponPlatformer _self;

        private void Awake()
        {
            _self = (WeaponPlatformer) target;
        }

        private void OnSceneGUI()
        {
            Debug.DrawRay(_self.transform.TransformPoint(_self.Offset), _self.transform.forward * _self.Height, Color.magenta);
            Debug.DrawRay(_self.transform.TransformPoint(_self.Offset) + _self.transform.right * _self.Radius, _self.transform.forward * _self.Height, Color.magenta);
            Debug.DrawRay(_self.transform.TransformPoint(_self.Offset) - _self.transform.right * _self.Radius, _self.transform.forward * _self.Height, Color.magenta);
            Debug.DrawRay(_self.transform.TransformPoint(_self.Offset) + _self.transform.up * _self.Radius, _self.transform.forward * _self.Height, Color.magenta);
            Debug.DrawRay(_self.transform.TransformPoint(_self.Offset) - _self.transform.up * _self.Radius, _self.transform.forward * _self.Height, Color.magenta);

            EditorGUI.BeginChangeCheck();
            var offset = Handles.DoPositionHandle(_self.transform.TransformPoint(_self.Offset), Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_self, "Move Offset");
                EditorUtility.SetDirty(_self);
                _self.Offset = _self.transform.InverseTransformPoint(offset);
            }
        }
    }
}
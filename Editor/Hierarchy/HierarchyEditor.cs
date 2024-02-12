#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ACustomEditor
{
    [InitializeOnLoad]
    public class HierarchyEditor : Editor
    {
        private static Dictionary<GameObject, string> emptys = new Dictionary<GameObject, string>();

        static HierarchyEditor()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyGroupOnGUI;
        }

        static void HierarchyGroupOnGUI(int instanceID, Rect selectionRect)
        {
            /// ------------------------------------------------ ///
            ///Custom your path
            string pathScriptableHierarchy = "Assets/ToolsEditor/Hierarchy/Scripts/HierarchysCustomization.asset";
            /// ------------------------------------------------ ///
            /// 

            HierarchysCustomization hierarchyCustomization = AssetDatabase.LoadAssetAtPath<HierarchysCustomization>(pathScriptableHierarchy);
            
            if(hierarchyCustomization == null) return;
            
            var empty = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (empty == null) return;

            if (emptys.Count != 0 && !emptys.ContainsKey(empty))
                emptys.Add(empty, "");

            foreach (var h in hierarchyCustomization.HierarchysCustomizations)
            {
                // DRAW Childreen
                if (empty.transform.root != null &&
                    empty.transform.root.name.StartsWith(h.Prefix, StringComparison.Ordinal) &&
                    !empty.name.StartsWith(h.Prefix, StringComparison.Ordinal))
                {
                    //Child Label
                    EditorGUI.DrawRect(selectionRect, h.LabelChildBackgroundColor);

                    //Bar Hierarchy
                    //Vertical BAR
                    Rect rectBar = new Rect(selectionRect.position + new Vector2(-selectionRect.size.y - 7f, -6f),
                        new Vector2(2, selectionRect.size.y - 3));
                    Color borderColor = h.barColor;
                    Texture2D borderTexture = new Texture2D(1, 1);
                    borderTexture.SetPixel(0, 0, borderColor);
                    borderTexture.Apply();
                    GUI.DrawTexture(rectBar, borderTexture);

                    //Horizontal BAR
                    rectBar = new Rect(selectionRect.position + new Vector2(-selectionRect.size.y - 7f, 6f),
                        new Vector2(selectionRect.size.y - 4f, 2));
                    GUI.DrawTexture(rectBar, borderTexture);
                }

                if (!empty.name.StartsWith(h.Prefix, StringComparison.Ordinal)) continue;

                //Border
                EditorGUI.DrawRect(selectionRect, h.LabelBorderColor);

                //Label
                EditorGUI.DrawRect(
                    new Rect(new Rect(selectionRect.position + new Vector2(2f, 1f),
                        selectionRect.size - new Vector2(4f, 4f))), h.LabelColor);

                //Icon
                Rect rectIcon = new Rect(selectionRect.position + new Vector2(3, 0),
                    new Vector2(selectionRect.size.y, selectionRect.size.y));
                GUI.DrawTexture(rectIcon, h.TextureIcon);

                //Second Icon
                Rect rectSecondIcon =
                    new Rect(selectionRect.position + new Vector2(selectionRect.size.x - selectionRect.size.y, 0),
                        new Vector2(selectionRect.size.y, selectionRect.size.y));
                GUI.DrawTexture(rectSecondIcon, h.TextureIcon);

                //Vertical Bar
                Rect recBar = new Rect(rectIcon.position + new Vector2(rectIcon.size.y + 3f, 0),
                    new Vector2(2, rectIcon.size.y));
                Color colorBar = Color.white;
                Texture2D barTexture = new Texture2D(1, 1);
                barTexture.SetPixel(0, 0, colorBar);
                barTexture.Apply();
                GUI.DrawTexture(recBar, barTexture);

                //Second Vertical Bar
                recBar = new Rect(rectIcon.position + new Vector2(selectionRect.size.x - (rectIcon.size.y + 6f), 0),
                    new Vector2(2, rectIcon.size.y));
                barTexture.SetPixel(0, 0, colorBar);
                barTexture.Apply();
                GUI.DrawTexture(recBar, barTexture);

                /// Name Label
                EditorGUI.DropShadowLabel(selectionRect, empty.name.Replace(h.Prefix, "").ToUpperInvariant());
            }
        }
    }
}

#endif
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ACustomEditor
{
    [CreateAssetMenu(fileName = "HierarchysCustomization", menuName = "HierarchysCustomization", order = 1)]
    public class HierarchysCustomization : ScriptableObject
    {
        public List<HierarchyCustomization> HierarchysCustomizations;
    }

    [Serializable]
    public class HierarchyCustomization
    {
        public string Prefix = "-";
        public Color LabelColor = Color.red;
        public Color LabelBorderColor = Color.red;
        public Color LabelChildBackgroundColor = Color.red;
        public Color barColor = Color.red;
        public Texture TextureIcon;
    }
}
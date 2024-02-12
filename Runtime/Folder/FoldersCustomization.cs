using System;
using System.Collections.Generic;
using UnityEngine;

namespace ACustomEditor
{
    [CreateAssetMenu(fileName = "FoldersCustomization", menuName = "FoldersCustomization", order = 1)]
    public class FoldersCustomization : ScriptableObject
    {
        public List<FolderCustomization> FoldersCustomizations;
    }

    [Serializable]
    public class FolderCustomization
    {
        public string FolderName = "FolderName";
        public Texture TextureFolder;
        public Texture TextureIcon;
    }
}
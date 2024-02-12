using UnityEditor;
using UnityEngine;

namespace ACustomEditor
{
    [InitializeOnLoad]
    public class FolderAssetProcessor : AssetPostprocessor
    {
        static FolderAssetProcessor()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        //Draw Folder and icon 
        private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            
            /// ------------------------------------------------ ///
            ///Custom your path
            string folderCustomizationPath = "Assets/ToolsEditor/Folders/Scripts/FoldersCustomization.asset";
            /// ------------------------------------------------ ///
            
            FoldersCustomization folderCustomisation = AssetDatabase.LoadAssetAtPath<FoldersCustomization>(folderCustomizationPath);

            if(folderCustomisation == null) return;
            
            foreach (FolderCustomization folderPerso in folderCustomisation.FoldersCustomizations)
            {
                if (AssetDatabase.IsValidFolder(assetPath) && assetPath.EndsWith("/" + folderPerso.FolderName))
                {
                    if (!IsMainListAsset(selectionRect))
                    {
                        //hierarchy folder
                        GUI.DrawTexture(new Rect(selectionRect.x, selectionRect.y, 16, 16), folderPerso.TextureFolder);
                        return;
                    }
            
                    //folder
                    GUI.DrawTexture(new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.height - 20f), folderPerso.TextureFolder);
                
                    //folder icon
                    GUI.DrawTexture(new Rect(selectionRect.x + 25f, selectionRect.y + 20f, 25f, 25f), folderPerso.TextureIcon);
                }
            }
        }

        // Don't draw in hierarchy project
        private static bool IsMainListAsset(Rect rect) => rect.height > 16f;
    }    
}

using UnityEditor;

public class ImportOverride : AssetPostprocessor
{
    void OnPreprocessModel()
    {
        var importer = assetImporter as ModelImporter;
        importer.importMaterials = false;
    }
}

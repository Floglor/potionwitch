using Alchemy;
using Garden;
using UnityEditor;

namespace Misc
{
    public static class AssetCreator
    {
        
        public static void CreateSeed(Ingredient ingredient, string folderPath, string fileName, string seedName)
        {
            GardenSeed seed = new GardenSeed();
            seed.Name = seedName;
            seed.GrowthDays = 2;
            seed.EditorSetIngredient(ingredient);

            string assetPath = $"{folderPath}/{fileName}.asset";
            AssetDatabase.CreateAsset(seed, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
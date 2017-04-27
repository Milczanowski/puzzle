using UnityEngine;

namespace Assets.Scripts.AssetsManagers
{
    public static class AssetsManager
    {
        #region Fields 
        private static AssetsLoader<Texture2D> puzzleImages;
        #endregion 


        #region Properties
        public static AssetsLoader<Texture2D> PuzzleImages
        {
            get { return puzzleImages; }
        }


        #endregion

        static AssetsManager()
        {
            puzzleImages = new AssetsLoader<Texture2D>(Settings.PuzzleFolder);
        }

        public static void Clear()
        {
            puzzleImages.Clear();
        }

    }
}

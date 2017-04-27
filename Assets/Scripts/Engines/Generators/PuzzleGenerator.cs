using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Engines.Generators
{
    class PuzzleGenerator: MonoBehaviour
    { 
        private static PuzzleGenerator instance { get; set; }

        [SerializeField]
        Texture2D PuzzleMask;

        [SerializeField]
        Texture2D CornerMask;

        [SerializeField]
        Texture2D SideMask;

        [SerializeField]
        Puzzle puzzlePrefab;

        [SerializeField]
        Transform PuzzleParent;

        private  static Texture2D Texture { get; set; }

        

        private void Awake()
        {
            if (!instance) instance = this;
        }



        public static IEnumerator Generate(Texture2D texture, Action onFinish, Action<float> onProgress)
        {
            Texture = texture;
            instance.PuzzleParent.position = Vector3.zero;

            Layers puzzleLayers = new Layers(1, 1);
            Layers cornerLayers = new Layers(1, 1);
            Layers sideLayers = new Layers(1, 1);

            yield return GetMask(instance.PuzzleMask, (layers) => { puzzleLayers = layers; });
            onProgress(.05f);
            yield return GetMask(instance.CornerMask, (layers) => { cornerLayers = layers; });
            onProgress(.1f);
            yield return GetMask(instance.SideMask, (layers) => { sideLayers = layers; });
            onProgress(.15f);

            int puzzleSize = (int)(Settings.PuzzleSize / 6.0f) * 4;
            int startX = (int)((Texture.width % puzzleSize) / 2f);
            int startY = (int)((Texture.height % puzzleSize) / 2f);

            int maxX = GameEngine.W = Texture.width / puzzleSize;
            int maxY = GameEngine.H = Texture.height / puzzleSize;

            GameEngine.Puzzle = new Puzzle[GameEngine.W, GameEngine.H];

            float progress = 0;

            for (int x = -startX, indexX = 0; indexX < maxX; x += puzzleSize, ++indexX)
            {
                for (int y = -startY, indexY = 0; indexY < maxY; y += puzzleSize, ++indexY)
                {
                    if (indexX == 0)
                    {
                        if (indexY == 0)
                            Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), cornerLayers, Layer.A),
                                instance.PuzzleParent, indexX, indexY, 2);
                        else if (indexY == maxY - 1)
                            Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), cornerLayers, Layer.R),
                                instance.PuzzleParent, indexX, indexY, 2);
                        else
                            Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), sideLayers, Layer.A),
                                instance.PuzzleParent, indexX, indexY, 3);

                    }
                    else if (indexX == maxX - 1)
                    {
                        if (indexY == 0)
                            Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), cornerLayers, Layer.B),
                                instance.PuzzleParent, indexX, indexY, 2);
                        else if (indexY == maxY - 1)
                            Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), cornerLayers, Layer.G),
                                instance.PuzzleParent, indexX, indexY, 2);
                        else
                            Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), sideLayers, Layer.G),
                                instance.PuzzleParent, indexX, indexY, 3);
                    }
                    else if (indexY == 0)
                        Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), sideLayers, Layer.B),
                                instance.PuzzleParent, indexX, indexY, 3);
                    else if (indexY == maxY - 1)
                        Instantiate(instance.puzzlePrefab).Set(GetMaskSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX)), sideLayers, Layer.R),
                                instance.PuzzleParent, indexX, indexY, 3);
                    else
                        Instantiate(instance.puzzlePrefab).Set(GetSprite(Texture, x, y, puzzleLayers.Get(GetLayer(indexY, indexX))),
                            instance.PuzzleParent, indexX, indexY, 4);

                    onProgress(0.15f + (0.85f * (progress / (maxX * maxY))));
                    progress++;
                }

                yield return null;
            }

            Texture = null;

            instance.PuzzleParent.position = new Vector3(-maxX * Settings.PuzzleShift / 2, -maxY * Settings.PuzzleShift / 2);
            onFinish();
        }

        private static Layer GetLayer(int x, int y)
        {
            if(x % 2 ==0)
            {
                if (y % 2 == 0)
                    return Layer.B;
                return Layer.A;
            }else
            {
                if (y % 2 == 0)
                    return Layer.R;
                return Layer.G;
            }
        }

        private static Sprite GetMaskSprite(Texture2D texture, int startX, int startY, bool[,] mask, Layers layers, Layer layer)
        {
            bool[,] newMask = layers.Sum(layer, mask);
            return GetSprite(texture, startX, startY, newMask);
        }

        private static Sprite GetSprite(Texture2D texture,int startX, int startY, bool[,] mask)
        {
            Texture2D puzzleTexture = new Texture2D(Settings.PuzzleSize, Settings.PuzzleSize, TextureFormat.RGBA32, true);
            Color alpha = new Color(0, 0, 0, 0);

            for (int x = 0; x < Settings.PuzzleSize; ++x)
            {
                for (int y = 0; y < Settings.PuzzleSize; ++y)
                {
                    if (x + startX > texture.width || x + startX < 0 || y + startY > texture.height || y + startY < 0)
                        puzzleTexture.SetPixel(x, y, alpha);
                    else
                        puzzleTexture.SetPixel(x, y, mask[x,y] ? texture.GetPixel(x + startX, y + startY) : alpha);
                }
            }
            puzzleTexture.Apply();
            return Texture2DToSprite(puzzleTexture);
        }

        private static IEnumerator GetMask(Texture2D texture, Action<Layers> outMask)
        {
            Layers layers = new Layers(texture.width, texture.height);

            for (int x = 0; x < texture.width; ++x)
            {
                for (int y = 0; y < texture.height; ++y)
                {
                    Color color = texture.GetPixel(x, y);
                    layers.R[x, y] = color.r == 1;
                    layers.G[x, y] = color.g == 1;
                    layers.B[x, y] = color.b == 1;
                    layers.A[x, y] = color.a == 1;
                }
                if(x % 20 ==0) yield return null;
            }
            outMask(layers);
        }

        private static Sprite Texture2DToSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
        }
    }
}

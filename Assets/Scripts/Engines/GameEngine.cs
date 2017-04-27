using Assets.Scripts.AssetsManagers;
using Assets.Scripts.Engines.Generators;
using Assets.Scripts.States;
using Assets.Scripts.States.AppState;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Engines
{
    class GameEngine
    {
        public static Vector3 Position { get; private set; }
        public static Vector3 Delta { get; private set; }
        public static int SelectImageIndex { get; set; }
        public static Puzzle[,] Puzzle { get; set; }
        public static int W { get; set; }
        public static int H { get; set; }
        public static Action OnDone { get; set; }

        public static Puzzle selected;
        public static Puzzle Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (((selected && !value) || (!selected && value)) && State.CheckState<GameState>())
                {
                    if (selected) selected.HighlightOff();
                    if (value) value.Highlight();

                    selected = value;
                }
            }
        }

        public static IEnumerator Generate(Action onFinish, Action<float> onProgress)
        {
            Texture2D texture = AssetsManager.PuzzleImages[Settings.PuzzleFile + SelectImageIndex];

            yield return PuzzleGenerator.Generate(texture, onFinish, onProgress);
            yield return RandomPuzzle();
        }

        public static void Clear()
        {
            if (Puzzle != null)
                for (int x = 0; x < W; ++x)
                for (int y = 0; y < H; ++y)
                    MonoBehaviour.Destroy(Puzzle[x,y].gameObject);

            Puzzle = null;
        }

        public static void Connect()
        {
            if (Puzzle != null)
                for (int x = 0; x < W; ++x)
                {
                    for (int y = 0; y < H; ++y)
                    {
                        if (x - 1 >= 0) Puzzle[x, y].Connected.Add(Puzzle[x - 1, y]);
                        if (x + 1 < W) Puzzle[x, y].Connected.Add(Puzzle[x + 1, y]);
                        if (y - 1 >= 0) Puzzle[x, y].Connected.Add(Puzzle[x, y - 1]);
                        if (y + 1 < H) Puzzle[x, y].Connected.Add(Puzzle[x, y + 1]);
                    }
                }
        }

        private static IEnumerator RandomPuzzle()
        {
            int[] angle = new int[] { 0, 90, 180, 270 };

            Dictionary<int,List<int>> positions = new Dictionary<int, List<int>>();

            if (Puzzle != null)
                for (int x = 0; x < W; ++x)
                {
                    for (int y = 0; y < H; ++y)
                    {
                        Puzzle[x, y].transform.eulerAngles = new Vector3(0, 0, angle[UnityEngine.Random.Range(0, 3)]);

                        while (true)
                        {
                            int w = UnityEngine.Random.Range(0, W);
                            int h = UnityEngine.Random.Range(0, W);

                            if(positions.ContainsKey(w))
                            {
                                if(!positions[w].Contains(h))
                                {
                                    positions[w].Add(h);
                                    Puzzle[x, y].transform.localPosition = new Vector2(w * Settings.PuzzleRandomShift, h * Settings.PuzzleRandomShift);
                                    break;
                                }
                            }else
                            {
                                positions.Add(w, new List<int>() {h});
                                Puzzle[x, y].transform.localPosition = new Vector2(w * Settings.PuzzleRandomShift, h * Settings.PuzzleRandomShift);
                                break;
                            }
                        }
                    }
                    yield return null;
                }
        }

        public static void SetPuzzleEnabled(bool enabled)
        {
            if (Puzzle != null)
                for (int x = 0; x < W; ++x)
                    for (int y = 0; y < H; ++y)
                        Puzzle[x, y].enabled = enabled;
        }

        public static void SetRecursion(bool recu)
        {
            if (Puzzle != null)
                for (int x = 0; x < W; ++x)
                    for (int y = 0; y < H; ++y)
                        Puzzle[x, y].IsRecu = recu;
        }

        public static void Update()
        {
            Vector3 pos = Input.mousePosition;
            pos.z = -UnityEngine.Camera.main.transform.position.z;
            Delta = Position - Camera.main.ScreenToWorldPoint(pos);
            Position = Camera.main.ScreenToWorldPoint(pos);
        }

        public static bool Done()
        {
            if (Puzzle != null)
                for (int x = 0; x < W; ++x)
                    for (int y = 0; y < H; ++y)
                        if (!Puzzle[x, y].IsConnected) return false;
            return true;
        }
    }
}

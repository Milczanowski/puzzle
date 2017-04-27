using Assets.Scripts.Menu;
using Assets.Scripts.States;
using Assets.Scripts.States.AppState;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Engines
{
    class Puzzle:MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer Renderer;
        public int X { get; set; }
        public int Y { get; set; }
        protected internal List<Puzzle> Connected { get; private set; }
        public bool IsRecu { get; set; }
        private int RequiredConnection { get; set; }

        public bool IsConnected
        {
            get
            {
                return Connected.Count == RequiredConnection;
            }
        }

        public void Set(Sprite sprite, Transform parent, int x, int y, int requiredConnection)
        {
            if (!Renderer) Renderer = GetComponent<SpriteRenderer>();

            transform.position = new Vector2(x * Settings.PuzzleRandomShift, y * Settings.PuzzleRandomShift);
            Renderer.sprite = sprite;
            transform.SetParent(parent);
            Connected = new List<Puzzle>();

            IsRecu = false;
            RequiredConnection = requiredConnection;


            X = x;
            Y = y;
            GameEngine.Puzzle[x, y] = this;
        }

        private void OnMouseOver()
        {
            if (State.CheckState<GameState>())
            {
                GameEngine.Selected = this;

                PuzzleMenu.SetAction(Rotate, Move, Up, transform.position);
                PuzzleMenu.SetActive(true);
            }
        }
        
        private void Move()
        {
            if (State.CheckState<GameState>())
            { 
                transform.position -= GameEngine.Delta;
                PuzzleMenu.SetPosition(transform.position);
                Move(this);
                GameEngine.SetRecursion(false);
            }
        }

        private void OnMouseDrag()
        {
            Move();
        }

        private void OnMouseUp()
        {
            Up();
        }
        
        private void Rotate()
        {
            transform.eulerAngles += new Vector3(0, 0, -90);

            Move(this);
            GameEngine.SetRecursion(false);
        }

        private void Up()
        {
            if (State.CheckState<GameState>())
            {
                GameEngine.Selected = null;
                Connect();
                GameEngine.SetRecursion(false);
                Move(this);
                GameEngine.SetRecursion(false);

                if (GameEngine.Done())
                    GameEngine.OnDone();
            }
        }

        public void Move(Puzzle puzzle)
        {
            IsRecu = true;
            transform.position = puzzle.transform.position + Quaternion.Euler(puzzle.transform.eulerAngles) *new Vector3(Settings.PuzzleShift * (X - puzzle.X), Settings.PuzzleShift * (Y - puzzle.Y));
            transform.eulerAngles = puzzle.transform.eulerAngles;
            foreach (Puzzle p in Connected)
                if (!p.IsRecu) p.Move(puzzle);
        }

        public void SetHighlight(Color color, bool up, int layer)
        {
            Renderer.color = color;
            if (up)
                transform.position = new Vector3(transform.position.x, transform.position.y, -0.05f);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            gameObject.layer = layer;


            IsRecu = true;
            foreach (Puzzle p in Connected)
                if (!p.IsRecu) p.SetHighlight(color, up, layer);
        }

        public void Highlight()
        {
            SetHighlight(Color.white, true, 10);
            GameEngine.SetRecursion(false);
        }

        public void HighlightOff()
        {
            SetHighlight(new Color(0.90f, 0.90f, 0.90f, 1), false, 9);
            GameEngine.SetRecursion(false);
        }

        private void Connect()
        {
            if (X - 1 >= 0 && !Connected.Contains(GameEngine.Puzzle[X - 1, Y]))
                if (GameEngine.Puzzle[X - 1, Y].transform.eulerAngles.z == transform.eulerAngles.z && Vector3.Distance(GameEngine.Puzzle[X - 1, Y].transform.position,
                    transform.position + Quaternion.Euler(transform.eulerAngles) * new Vector3(-Settings.PuzzleShift, 0)) < 0.15f)
                {
                    Connected.Add(GameEngine.Puzzle[X - 1, Y]);
                    GameEngine.Puzzle[X - 1, Y].Connected.Add(this);
                }

            if (X + 1 < GameEngine.W && !Connected.Contains(GameEngine.Puzzle[X + 1, Y]))
                if (GameEngine.Puzzle[X + 1, Y].transform.eulerAngles.z == transform.eulerAngles.z && Vector3.Distance(GameEngine.Puzzle[X + 1, Y].transform.position,
                    transform.position + Quaternion.Euler(transform.eulerAngles) * new Vector3(Settings.PuzzleShift, 0)) < 0.15f)
                {
                    Connected.Add(GameEngine.Puzzle[X + 1, Y]);
                    GameEngine.Puzzle[X + 1, Y].Connected.Add(this);
                }

            if (Y - 1 >= 0 && !Connected.Contains(GameEngine.Puzzle[X, Y - 1]))
                if (GameEngine.Puzzle[X, Y - 1].transform.eulerAngles.z == transform.eulerAngles.z && Vector3.Distance(GameEngine.Puzzle[X, Y - 1].transform.position,
                    transform.position + Quaternion.Euler(transform.eulerAngles) * new Vector3(0, -Settings.PuzzleShift)) < 0.15f)
                {
                    Connected.Add(GameEngine.Puzzle[X, Y - 1]);
                    GameEngine.Puzzle[X, Y -1].Connected.Add(this);
                }

            if (Y + 1 < GameEngine.H && !Connected.Contains(GameEngine.Puzzle[X, Y + 1]))
                if (GameEngine.Puzzle[X, Y + 1].transform.eulerAngles.z == transform.eulerAngles.z && Vector3.Distance(GameEngine.Puzzle[X, Y + 1].transform.position,
                    transform.position + Quaternion.Euler(transform.eulerAngles) * new Vector3(0, Settings.PuzzleShift)) < 0.15f)
                {
                    Connected.Add(GameEngine.Puzzle[X, Y + 1]);
                    GameEngine.Puzzle[X, Y + 1].Connected.Add(this);
                }

            IsRecu = true;
            foreach (Puzzle p in Connected)
                if (!p.IsRecu) p.Connect();

        } 
    }
}

namespace Assets.Scripts.Engines.Generators
{
    class Layers
    {
        public bool[,] R { get; private set; }
        public bool[,] G { get; private set; }
        public bool[,] B { get; private set; }
        public bool[,] A { get; private set; }

        public int W { get; private set; }
        public int H { get; private set; }


        public Layers(int w, int h)
        {
            W = w;
            H = h;

            R = new bool[w, h];
            G = new bool[w, h];
            B = new bool[w, h];
            A = new bool[w, h];
        }

        public bool[,] Get(Layer layer)
        {
            switch (layer)
            {
                case Layer.R: return R;
                case Layer.G: return G;
                case Layer.B: return B;
                case Layer.A: return A;
            }
            return null;
        }

        public bool[,] Sum(Layer layer, bool[,] mask)
        {
            bool[,] newMask = new bool[W, H];

            for (int x = 0; x < W; ++x)
                for (int y = 0; y < H; ++y)
                    newMask[x, y] = Get(layer)[x, y] || mask[x, y];

            return newMask;
        }
    }
}

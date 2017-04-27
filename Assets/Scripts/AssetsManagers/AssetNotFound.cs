using System;

namespace Assets.Scripts.AssetsManagers
{
    class AssetNotFound: Exception
    {
        public AssetNotFound(string message) : base(message) { }

        public AssetNotFound() { }

    }
}

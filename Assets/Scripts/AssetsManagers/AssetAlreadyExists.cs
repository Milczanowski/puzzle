using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.AssetsManagers
{
    class AssetAlreadyExists : Exception
    {
        public AssetAlreadyExists(string message):base(message)
        {

        }
    }
}

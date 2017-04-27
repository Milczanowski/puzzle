using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Connections
{
    class FailData
    {
        private string error;

        public string Error
        {
            get { return error; }
        }

        public FailData(string error)
        {
            this.error = error;
        }
    }
}

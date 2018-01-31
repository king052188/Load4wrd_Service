using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace load4wrd_service
{
    public class RestartArgs : EventArgs
    {
        public bool IsRestart { get { return Restart.Proceed; } }
    }
}

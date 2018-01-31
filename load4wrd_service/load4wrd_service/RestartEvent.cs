using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace load4wrd_service
{
    public class RestartEvent
    {
        public delegate void RestartEventHandler(object sender, RestartArgs e);

        public event RestartEventHandler RestartProceed;
    }
}

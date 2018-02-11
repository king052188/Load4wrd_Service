using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l4dhelper
{
    public class StatusArgs : EventArgs
    {
        public int Code { get { return Status.code; } }
        public string Message { get { return Status.message; } }
        public string Smart { get { return Status.smart; } }
        public string Globe { get { return Status.globe; } }
        public string Sun { get { return Status.sunxx; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScripts.Logics.Message
{
    public interface ISimpleMessageReceiver
    {
        List<SimpleMessage> GetReceiver();
        List<SimpleMessage> Data();
    }
}

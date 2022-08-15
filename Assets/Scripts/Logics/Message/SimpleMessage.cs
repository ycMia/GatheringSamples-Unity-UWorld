using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScripts.Logics.Message
{
    //public class SimpleMessage
    //{
    //    public SimpleMessage(string initInfo)
    //    {
    //        info = initInfo;
    //    }
    //    public string info;
    //}

    public class SimpleMessage<T>
    {
        public SimpleMessage(T initInfo)
        {
            info = initInfo;
        }
        public T info;
    }
}

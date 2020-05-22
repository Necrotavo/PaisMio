using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UI.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Usuario" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Usuario.svc or Usuario.svc.cs at the Solution Explorer and start debugging.
    public class Usuario : IUsuario
    {
        public void DoWork()
        {

        }

        public List<string> getPrueba()
        {
            List<String> list = new List<string>();
            list.Add("a");
            list.Add("b");
            list.Add("c");
            return list;
        }
    }
}

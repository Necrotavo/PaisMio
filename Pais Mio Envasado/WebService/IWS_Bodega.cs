using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UI.WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWS_Bodega" in both code and config file together.
    [ServiceContract]
    public interface IWS_Bodega
    {
        [OperationContract]
        void DoWork();
    }
}

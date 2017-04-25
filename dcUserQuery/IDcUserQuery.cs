using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace dcUserQuery
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDcUserQuery" in both code and config file together.
    [ServiceContract]
    public interface IDcUserQuery
    {

        [OperationContract]
        List<string> GetMyGroups();

        //[OperationContract]
        //string GetUserGroups(string serverAddress, string domain, string userName, string password); 

        //[OperationContract]
        //string LoginUser(string ldapAddress, string domain, string username, string password );

        //[OperationContract]
        //List<string> GetUserDepartments(string serverAddress, string domain, string userName, string password); 


    }
}

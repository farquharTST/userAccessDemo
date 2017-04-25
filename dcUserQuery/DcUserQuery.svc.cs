using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;


namespace dcUserQuery
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DcUserQuery" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DcUserQuery.svc or DcUserQuery.svc.cs at the Solution Explorer and start debugging.
    public class DcUserQuery : IDcUserQuery
    {

        public List<string> GetMyGroups()
        {
            PrincipalSearchResult<Principal> groups = UserPrincipal.Current.GetGroups();

            return groups.Select(principal => principal.Name).ToList();
        }


        /// <summary>
        /// Will attempt to authenticate the user against the AD directory
        /// </summary>
        /// <param name="address"></param>
        /// <param name="domain"></param>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <param name="authMsg"></param>
        /// <returns></returns>
        private bool Authenticate(string address, string domain, string userName, string userPassword, out string authMsg)
        {
            bool authentic = false;
            authMsg = string.Empty;
            try
            {
                DirectoryEntry entry = new DirectoryEntry(address, userName, userPassword);
                object nativeObject = entry.NativeObject;
                authentic = true;
                //authMsg = entry.
            }
            catch (DirectoryServicesCOMException ex)
            {
                authMsg = ex.Message;
            }
            return authentic;
        }


        public ArrayList AttributeValuesMultiString(string attributeName, string objectDn, ArrayList valuesCollection, bool recursive)
        {
            DirectoryEntry ent = new DirectoryEntry(objectDn);
            PropertyValueCollection ValueCollection = ent.Properties[attributeName];
            IEnumerator en = ValueCollection.GetEnumerator();

            while (en.MoveNext())
            {
                if (en.Current != null)
                {
                    if (!valuesCollection.Contains(en.Current.ToString()))
                    {
                        valuesCollection.Add(en.Current.ToString());
                        if (recursive)
                        {
                            AttributeValuesMultiString(attributeName, "LDAP://" + en.Current.ToString(), valuesCollection, true);
                        }
                    }
                }
            }
            ent.Close();
            ent.Dispose();
            return valuesCollection;
        }

        #region Public Methods


        #endregion


    }
}

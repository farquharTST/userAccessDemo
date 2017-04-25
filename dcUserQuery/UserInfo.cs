using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

using System.Security.Principal;

namespace dcUserQuery
{
    /*
     * To use: 
            var uInfo = new UserInfo();
            var groups = uInfo.DomainGroups;
            var displayName = uInfo.DisplayName;
            var authGroups = uInfo.AuthDomainGroups;
     */

    [AttributeUsage(AttributeTargets.All)]
    public class TSTClassHelpAttribute : Attribute
    {
        public string ClassHelpText { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TSTMethodHelpAttribute: Attribute
    {
        public string HelpText { get; set; }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class TstMethodAccess: Attribute
    {
        public string warning { get; set; }
    }

    [TSTClassHelp(ClassHelpText = "Once instantiated, the class contains the user's Active Directory Information.")]
    public class UserInfo
    {
        #region Private Members

        private string _displayName;
        private string _surname;
        private bool _smartcardLogonRequired;
        private List<string> _domainGroups;
        private List<string> _domainAuthGroups;

        #endregion

        #region Properties

        public string DisplayName
        {
            get { return _displayName;  }
        }

        public string Surname
        {
            get { return _surname; }
        }

        public bool SmartCardRequired
        {
            get { return _smartcardLogonRequired; }
        }

        /// <summary>
        /// A list of Active Directory - Directory Services groups to which the current user is assigned
        /// </summary>
        public List<string> DomainGroups
        {
            get { return _domainGroups; }
        }

        /// <summary>
        /// <p>
        /// A list of Active Directory - Directory Services groups of which the current user is 
        /// a member, but not necessarily explicitly assigned.
        /// </p>
        /// <p>
        /// Does not include distribution groups.
        /// </p>
        /// </summary>
        public List<string> AuthDomainGroups
        {
            get { return _domainAuthGroups; }
        }
        #endregion

        
        public UserInfo()
        {
            _displayName = UserPrincipal.Current.Name;
            _surname = UserPrincipal.Current.Surname;
            _smartcardLogonRequired = UserPrincipal.Current.SmartcardLogonRequired;
            
            PrincipalSearchResult<Principal> groups = UserPrincipal.Current.GetGroups();
            if (groups.Any())
            {
                _domainGroups = new List<string>();
                foreach (var principal in groups)
                {
                    _domainGroups.Add(principal.Name);
                }
            }

            var authGroups = UserPrincipal.Current.GetAuthorizationGroups();
            if (authGroups.Any())
            {
                _domainAuthGroups = new List<string>();
                foreach (var authGroup in authGroups)
                {
                    _domainAuthGroups.Add(authGroup.Name);
                }
            }

        }

        public bool UserExists(string username)
        {
            // create a domain context
            var domain = new PrincipalContext(ContextType.Domain);

            // find the user
            var foundUser = UserPrincipal.FindByIdentity(domain, IdentityType.Name, username);

            return foundUser != null;
        }
    }
}
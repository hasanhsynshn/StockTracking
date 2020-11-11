using StockTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace StockTracking.Roles
{
    public class UserRoles : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        StockTrackingEntities c = new StockTrackingEntities();
        public override string[] GetRolesForUser(string username)
        {
            List<UserRole> userRoles = c.UserRole.Where(x => x.User.UserName == username).ToList();
            string[] roles = new string[userRoles.Count];
            if (userRoles.Count>0)
            {
                for (int i = 0; i < roles.Length; i++)
                {
                    foreach (var x in userRoles)
                    {
                        roles[i] = x.Role.Role1.Trim();
                        userRoles.Remove(x);
                        break;

                    }
                }
                return roles;
            }
            return new string[] { "" };
            //var user = c.User.FirstOrDefault(x => x.UserName == username);
            //return new string[] { user.Role };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
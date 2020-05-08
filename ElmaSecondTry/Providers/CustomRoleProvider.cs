using ElmaSecondTry.App_Start;
using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using Ninject;
using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;

namespace ElmaSecondTry.Providers
{
    /// <summary>
    /// Класс провайдера ролей, необходим для работы с ролями
    /// </summary>
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Определение соответствуует ли роль пользователя определенной роли
        /// </summary>
        /// <param name="login"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override bool IsUserInRole(string login, string roleName)
        {
            var userRepository = NinjectWebCommon.Kernel.Get<IUserRepository>();
            var repositoryResult = userRepository.FindUser(login);
            return repositoryResult.Status == ActionStatus.Success && (repositoryResult.Entity.First() as UserBase).Role.ToString() == roleName;
        }

        /// <summary>
        /// Получение списка ролей пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string login)
        {
            var userRepository = NinjectWebCommon.Kernel.Get<IUserRepository>();
            var repositoryResult = userRepository.FindUser(login);
            return repositoryResult.Status == ActionStatus.Success 
                ? new string[] { (repositoryResult.Entity.First() as UserBase).Role.ToString() } 
                : new string[0];
        }

        public override string[] GetUsersInRole(string roleName) => throw new NotImplementedException();

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) => throw new NotImplementedException();

        public override void CreateRole(string roleName) => throw new NotImplementedException();

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) => throw new NotImplementedException();

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) => throw new NotImplementedException();

        public override string[] GetAllRoles() => throw new NotImplementedException();

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) => throw new NotImplementedException();

        public override bool RoleExists(string roleName) => throw new NotImplementedException();
        }
}
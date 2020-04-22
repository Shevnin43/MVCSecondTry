using ElmaSecondTryBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.IRepositories
{
    public interface IUserRepository
    {
        UserBase CreateUser(UserBase entity);
        bool DeleteUser(Guid id);
        UserBase UpdateUser(UserBase entity);
        IEnumerable<UserBase> FilterUsers(UserBase user, (DateTime Min, DateTime Max) period);
        UserBase FindUser(Guid id);
        UserBase FindUser(string login);
    }
}

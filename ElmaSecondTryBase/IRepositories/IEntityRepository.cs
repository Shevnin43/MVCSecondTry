using ElmaSecondTryBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.Repositories
{
    public interface IEntityRepository
    {
        UserBase CreateUser(UserBase entity);
        bool DeleteUser(Guid id);
        UserBase UpdateUser(UserBase entity);
        IEnumerable<UserBase> GetUsers();
        UserBase FindUser(Guid id);
    }
}

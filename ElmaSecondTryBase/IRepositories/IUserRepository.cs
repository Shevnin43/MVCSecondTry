using ElmaSecondTryBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.IRepositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с пользователя БД
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        RepositoryResult CreateUser(UserBase entity);
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        RepositoryResult DeleteUser(UserBase entity);
        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        RepositoryResult UpdateUser(UserBase entity);
        /// <summary>
        /// Выборка пользователей из БД
        /// </summary>
        /// <param name="user"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        RepositoryResult FilterUsers(UserBase user, Dictionary<string,DateTime> period);
        /// <summary>
        /// Найти пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <param name="withBlockedAnnouncements"></param>
        /// <returns></returns>
        RepositoryResult FindUser(string login, bool withBlockedAnnouncements = false);
    }
}

using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElmaSecondTryNHibernate.Repositories
{
    /// <summary>
    /// Класс репозитория для работы сущности UserBase с базой данных
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Конструктор с получением SessionFactory для работы с БД
        /// </summary>
        /// <param name="sessionFactory"></param>
        public UserRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public RepositoryResult CreateUser(UserBase entity)
        {
            if (entity==null)
            {
                return new RepositoryResult {Status = ActionStatus.Error, Message = $"К сожалению, регистрируемый пользователь пуст.", Entity = null};
            }
            entity.RegisterDate = DateTime.Now;
            entity.Role =UserRoles.None;
            var session = _sessionFactory.OpenSession();
            try
            {
                using (session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    session.Transaction.Commit();
                    var savedUser = session.Get<UserBase>(entity.Id);
                    session.Close();
                    return savedUser == null ? new RepositoryResult { Status = ActionStatus.Error, Message = $"Пользователя ({entity.Login}) зарегистрировать не удалось.", Entity = null } : new RepositoryResult { Status = ActionStatus.Success, Message = $"Пользователь {savedUser.Login} успешно зарегистрирован {savedUser.RegisterDate}.", Entity = new UserBase[] {savedUser} };
                }
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При регистрации пользователя ({entity.Login}) произошла непредвиденная ошибка репозитория. Исключение:{ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Удаление пользователя из базы данных
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public RepositoryResult DeleteUser(UserBase entity)
        {
            if (entity.NotKill)
            {
                return new RepositoryResult { Status = ActionStatus.Warning, Message = $"Не могу удалить пользователя ({entity.Login}). Данный пользователь помечен как неудаляемый.", Entity = null };
            }
            var session = _sessionFactory.OpenSession();
            try
            {
                using (session.BeginTransaction())
                {
                    session.Delete(entity);
                    session.Transaction.Commit();
                    session.Close();
                }
                return FindUser(entity.Login).Status != ActionStatus.Success ? new RepositoryResult { Status = ActionStatus.Success, Message = $"Пользователь ({entity.Login}) успешно удален.", Entity = null } : new RepositoryResult { Status = ActionStatus.Warning, Message = $"Не удалось удалить пользователя {entity.Login}.", Entity = null }; ; 
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При удалении пользователя ({entity.Login}) произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Извлечение пользователя из базы данных по Логину (по умолчанию - без объявлений со статусом "Заблокированные")
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public RepositoryResult FindUser(string login, bool withBlockedAnnouncements = false)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указан логин искомого пользователя.", Entity = null };
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                var user = session.QueryOver<UserBase>().Where(x => x.Login == login)?.SingleOrDefault();
                session.Close();
                if (user!=null && !withBlockedAnnouncements)
                {
                    user.Announcements = user.Announcements.Where(x => !x.IsBlocked).ToList();
                }
                return user==null ? new RepositoryResult { Status = ActionStatus.Warning, Message = $"Искомый пользователь ({login}) не найден.", Entity = null } : new RepositoryResult { Status = ActionStatus.Success, Message = $"Пользователь {login} успешно найден.", Entity = new UserBase[]{user} }; ;
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При извлечении пользователя ({login}) произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Выборка пользователей
        /// </summary>
        /// <param name="user"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public RepositoryResult FilterUsers(UserBase user, Dictionary<string,DateTime> period)
        {
            if (user==null || period ==null)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указаны параметры выборки пользователей.", Entity = null };
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                var criteria = session.CreateCriteria(typeof(UserBase))
                    .Add(LikeOrNull("Name", user.Name))
                    .Add(LikeOrNull("Login", user.Login))
                    .Add(LikeOrNull("About", user.About))
                    .Add(LikeOrNull("Phone", user.Phone))
                    .Add(LikeOrNull("Email", user.Email))
                .Add(Restrictions.Between("RegisterDate", period["min"], period["max"]));
                if (user.Role != UserRoles.All)
                {
                    criteria.Add(Restrictions.Like("Role", user.Role));
                }
                var result = criteria.List<UserBase>();
                session.Close();
                return result.Count==0 ? new RepositoryResult { Status = ActionStatus.Warning, Message = $"Искомые пользователи не найдены.", Entity = null } : new RepositoryResult { Status = ActionStatus.Success, Message = $"По запросу найдено {result.Count} пользователей.", Entity = result.ToArray() }; ;
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При извлечении выборке пользователей произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public RepositoryResult UpdateUser(UserBase entity)
        {
            if (entity == null)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указан пользователь, данные которого необходимо обновить.", Entity = null };
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                using (session.BeginTransaction())
                {
                    session.Update(entity);
                    session.Transaction.Commit();
                }
                var updatedUser = session.Get<UserBase>(entity.Id);
                session.Close();
                return updatedUser == entity 
                    ? new RepositoryResult { Status = ActionStatus.Success, Message = $"Пользователь ({entity.Login}) успешно обновлен.", Entity = new UserBase[] {updatedUser} } 
                    : new RepositoryResult { Status = ActionStatus.Warning, Message = $"Не удалось обновить данные пользователя {entity.Login}.", Entity = null };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При обновлении данных пользователя ({entity.Login}) произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Проверка значений поля и модели на нулл (для фильтрации по базе)
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private AbstractCriterion LikeOrNull(string property, object value)
        {
            return value == null ? Restrictions.Or(Restrictions.Ge(property,null), Restrictions.Ge(property, "")) : Restrictions.Like(property, value.ToString(), MatchMode.Anywhere);
        }

    }
}

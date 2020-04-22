using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Enums;
using ElmaSecondTryBase.IRepositories;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElmaSecondTryNHibernate.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public UserRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public UserBase CreateUser(UserBase entity)
        {
            if (entity==null)
            {
                return null;
            }
            var session = _sessionFactory.OpenSession();
            entity.RegisterDate = DateTime.Now;
            entity.Role =UserRoles.None;
            try
            {
                using (session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    session.Transaction.Commit();
                    var savedUser = session.Get<UserBase>(entity.Id);
                    session.Close();
                    return savedUser;
                }
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(Guid id)
        {
            if (id==null || id==Guid.Empty)
            {
                return false;
            }
            var savedUser = FindUser(id);
            var session = _sessionFactory.OpenSession();
            try
            {
                using (session.BeginTransaction())
                {
                    session.Delete(savedUser);
                    session.Transaction.Commit();
                    session.Close();
                }
                return FindUser(id) == null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Поиск пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserBase FindUser(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return null;
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                var user = session.Get<UserBase>(id);
                session.Close();
                return user;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Поиск пользователя по Логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public UserBase FindUser(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                return null;
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                var user = session.QueryOver<UserBase>().Where(x => x.Login == login)?.SingleOrDefault();
                session.Close();
                return user;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Фильтрация пользователей
        /// </summary>
        /// <param name="user"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public IEnumerable<UserBase> FilterUsers(UserBase user, (DateTime Min, DateTime Max) period)
        {
            if (user==null)
            {
                return new List<UserBase>();
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                var criteria = session.CreateCriteria(typeof(UserBase))
                    .Add(LikeOrNull("Name", user.Name))
                    .Add(LikeOrNull("Login", user.Login))
                    .Add(LikeOrNull("About", user.About))
                    .Add(LikeOrNull("Phone", user.Phone))
                    .Add(LikeOrNull("Email", user.Email));
                /*.Add(Expression.Between("RegisterDate", period.Min, period.Max));*/
                if (user.Role != UserRoles.All)
                {
                    criteria.Add(Restrictions.Eq("Role", (int)user.Role));
                }
                var result = criteria.List<UserBase>();
                session.Close();
                return result;
            }
            catch
            {
                return new List<UserBase>();
            }
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public UserBase UpdateUser(UserBase entity)
        {
            if (entity == null)
            {
                return null;
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
                return updatedUser;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Проверка значений поля и модели на нулл
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

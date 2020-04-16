using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryNHibernate.Repositories
{
    public class EntityRepository : IEntityRepository 
    {
        private readonly ISession _session;
        
        public EntityRepository(ISession session)
        {
            _session = session;
        }

        public UserBase CreateUser(UserBase entity)
        {
            using (_session.BeginTransaction())
            {
                _session.SaveOrUpdate(entity);
                _session.Transaction.Commit();
            }
            var savedUser= _session.Get<UserBase>(entity.Id);
            _session.Close();
            return savedUser;
        }

        public bool DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserBase FindUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserBase> GetUsers()
        {
            throw new NotImplementedException();
        }

        public UserBase UpdateUser(UserBase entity)
        {
            throw new NotImplementedException();
        }
    }
}

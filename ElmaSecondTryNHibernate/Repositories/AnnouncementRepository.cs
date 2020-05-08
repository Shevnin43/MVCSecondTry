using ElmaSecondTryBase.Entities;
using ElmaSecondTryBase.IRepositories;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElmaSecondTryNHibernate.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ISessionFactory _sessionFactory;

        /// <summary>
        /// Конструктор с получением SessionFactory для работы с БД
        /// </summary>
        /// <param name="sessionFactory"></param>
        public AnnouncementRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="savingEntity"></param>
        /// <returns></returns>
        public RepositoryResult CreateAnnouncement(IAnnouncement savingEntity) 
        {
            if (savingEntity == null)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указано создаваемое объявление.", Entity = null };
            }
            savingEntity.LastEdited = DateTime.Now;
            savingEntity.CreationDate = savingEntity.LastEdited;
            savingEntity.Id = Guid.NewGuid();
            try
            {
                var session = _sessionFactory.OpenSession();
                using (session.BeginTransaction())
                {
                    session.Save(savingEntity);
                    session.Transaction.Commit();
                }
                var savedEntity = session.Get<IAnnouncement>(savingEntity.Id);
                session.Close();
                return savedEntity == savingEntity
                ? new RepositoryResult { Status = ActionStatus.Success, Message = $"Объявление ({savingEntity.Id}) успешно добавлено.", Entity = new IAnnouncement[] { savedEntity } }
                : new RepositoryResult { Status = ActionStatus.Warning, Message = $"Не удалось обновить данные объявления {savedEntity.Id}.", Entity = null };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При обновлении данных объявления ({savingEntity.Id}) произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }


        /// <summary>
        /// Извлечение объявления 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResult FindAnnouncement(Guid id) 
        {
            if (id == null || id == Guid.Empty)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указан ай-ди объявления.", Entity = null };
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                var announcementFromDb = session.Get<IAnnouncement>(id);
                session.Close();
                return announcementFromDb == null ? new RepositoryResult { Status = ActionStatus.Warning, Message = $"Искомое объявление ({id}) не найдено.", Entity = null } : new RepositoryResult { Status = ActionStatus.Success, Message = $"Объявление {id} успешно найден.", Entity = new IAnnouncement[] { announcementFromDb } }; ;
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При извлечении объявления ({id}) произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Обновление объявления
        /// </summary>
        /// <param name="updatingAnnouncement"></param>
        /// <returns></returns>
        public RepositoryResult UpdateAnnouncement(IAnnouncement updatingAnnouncement)
        {
            if (updatingAnnouncement == null)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указано обновляемое объявление.", Entity = null };
            }
            updatingAnnouncement.LastEdited = DateTime.Now;
            try
            {
                var session = _sessionFactory.OpenSession();
                using (session.BeginTransaction())
                {
                    session.Update(updatingAnnouncement);
                    session.Transaction.Commit();
                }
                var updatedAnnouncement = session.Get<IAnnouncement>(updatingAnnouncement.Id);
                session.Close();
                return updatedAnnouncement == updatingAnnouncement
                ? new RepositoryResult { Status = ActionStatus.Success, Message = $"Объявление ({updatingAnnouncement.Id}) успешно обновлено.", Entity = new IAnnouncement[] { updatedAnnouncement } }
                : new RepositoryResult { Status = ActionStatus.Warning, Message = $"Не удалось обновить данные объявления {updatingAnnouncement.Id}.", Entity = null };
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При обновлении данных объявления ({updatingAnnouncement.Id}) произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Удаление объявления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RepositoryResult DeleteAnnouncement(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указан ай-ди объявления.", Entity = null };
            }
            var savedResult = FindAnnouncement(id);
            if (savedResult.Status != ActionStatus.Success)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"В базе данных объявление с идентификатором {id} не обнаружено.", Entity = null };
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                using (session.BeginTransaction())
                {
                    session.Delete(savedResult.Entity.First() as IAnnouncement);
                    session.Transaction.Commit();
                    session.Close();
                }
                return FindAnnouncement(id).Status != ActionStatus.Success ? new RepositoryResult { Status = ActionStatus.Success, Message = $"Объявление {id} успешно удалено.", Entity = null } : new RepositoryResult { Status = ActionStatus.Warning, Message = $"Не удалось удалить объявление {id}. ", Entity = null }; ;
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При удалении объявления {id} произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
        }

        /// <summary>
        /// Выборка объявлений из базы данных
        /// </summary>
        /// <param name="selectParams"></param>
        /// <returns></returns>
        public RepositoryResult SelectAnnouncements(Dictionary<string, object> selectParams)
        {
            if (selectParams == null || selectParams.Count == 0)
            {
                return new RepositoryResult { Status = ActionStatus.Error, Message = $"Не указаны параметры выборки объявлений.", Entity = null };
            }
            try
            {
                var session = _sessionFactory.OpenSession();
                var criteria = session.CreateCriteria(typeof(IAnnouncement));
                
                foreach (var param in selectParams)
                {
                    // отбрасываем все свойства которые не должны влиять на результат фильтрации
                    if (param.Value == null || (param.Key == "Employment" && param.Value.ToString()=="All"))
                    {
                        continue;
                    }
                    // Для всех свойств, имеющих в наименовании "Min" устанавливаем критерий "больше или равно чем..."
                    if (param.Key.Contains("Min"))
                    {
                        criteria.Add(Restrictions.Ge(param.Key.Replace("Min", ""), param.Value));
                        continue;
                    }
                    // Для всех свойств, имеющих в наименовании "Max" устанавливаем критерий "меньше или равно чем..."
                    if (param.Key.Contains("Max"))
                    {
                        criteria.Add(Restrictions.Le(param.Key.Replace("Max", ""), param.Value));
                        continue;
                    }
                    // Для свойств типа string или для свойства "Employment" устанавливаем свойство "на подобие как ..."
                    if (param.Value.GetType() == typeof(string))
                    {
                        criteria.Add(Restrictions.Like(param.Key, param.Value.ToString(), MatchMode.Anywhere));
                        continue;
                    }
                    // Для свойств типа bool 
                    if (param.Value.GetType() == typeof(bool) || param.Key == "Employment")
                    {
                        criteria.Add(Restrictions.Like(param.Key, param.Value));
                        continue;
                    }

                }

                var result = criteria.List<IAnnouncement>();
                session.Close();
                return result.Count == 0 ? new RepositoryResult { Status = ActionStatus.Warning, Message = $"Подходящие объявления не найдены.", Entity = null } : new RepositoryResult { Status = ActionStatus.Success, Message = $"По запросу найдено {result.Count} объявленний.", Entity = result.ToArray() }; ;
            }
            catch (Exception ex)
            {
                return new RepositoryResult { Status = ActionStatus.Fatal, Message = $"При извлечении выборке пользователей произошла непредвиденная ошибка репозитория. Исключение: {ex.Message}", Entity = null };
            }
            throw new NotImplementedException();
        }
    }
}

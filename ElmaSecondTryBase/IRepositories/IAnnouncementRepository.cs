using ElmaSecondTryBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElmaSecondTryBase.IRepositories
{
    public interface IAnnouncementRepository
    {
        /// <summary>
        /// Создание нового объявления
        /// </summary>
        /// <param name="savingEntity"></param>
        /// <returns></returns>
        RepositoryResult CreateAnnouncement(IAnnouncement savingEntity);

        /// <summary>
        /// Найти обявление
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResult FindAnnouncement(Guid id);

        /// <summary>
        /// Обновить объявление кандидата
        /// </summary>
        /// <param name="updatingCandidate"></param>
        /// <returns></returns>
        RepositoryResult UpdateAnnouncement(IAnnouncement updatingCandidate);

        /// <summary>
        /// Удаление объявления кандидата
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        RepositoryResult DeleteAnnouncement(Guid id);

        /// <summary>
        /// Выборка объектов из базы данных
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="selectParams"></param>
        /// <returns></returns>
        RepositoryResult SelectAnnouncements(Dictionary<string, object> selectParams);
    }
}

using AutoMapper;
using ElmaSecondTry.Models.Account;
using ElmaSecondTry.Models.User;
using ElmaSecondTry.Models.Vacancy;
using ElmaSecondTryBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElmaSecondTry.Helpers
{
    public static class Mappings
    {
        public static MapperConfiguration ConfigureMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Registration, UserBase>();
                cfg.CreateMap<Authorization, UserBase>();
                cfg.CreateMap<EditAccount, UserBase>();
                cfg.CreateMap<UserBase, EditAccount>();

                cfg.CreateMap<ShowUser, UserBase>();
                cfg.CreateMap<EditUser, UserBase>();
                cfg.CreateMap<FilterUser, UserBase>();
                cfg.CreateMap<UserBase, ShowUser>();
                cfg.CreateMap<UserBase, EditUser>();

                cfg.CreateMap<BaseVacancy, VacancyBase>();
                cfg.CreateMap<ShowVacancy, VacancyBase>();
            });

            return config;
        }
    }
}
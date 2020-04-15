using AutoMapper;
using ElmaSecondTry.Models.Account;
using ElmaSecondTry.Models.User;
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
                cfg.CreateMap<BaseUser, UserBase>();
                cfg.CreateMap<EditUser, UserBase>();
                cfg.CreateMap<ShowUser, UserBase>();

            });

            return config;
        }
    }
}
using AutoMapper;
using ElmaSecondTry.Models.AccountModel;
using ElmaSecondTry.Models;
using ElmaSecondTry.Models.CandidateModel;
using ElmaSecondTry.Models.UserModel;
using ElmaSecondTry.Models.VacancyModel;
using ElmaSecondTryBase.Entities;

namespace ElmaSecondTry.Helpers
{
    /// <summary>
    /// Класс маппинга моделей ElmaSecondTry.Base в модели ElmaSecondTry и обратно
    /// </summary>
    public static class Mappings
    {
        public static MapperConfiguration ConfigureMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Registration, UserBase>();
                cfg.CreateMap<Authorization, UserBase>();
                cfg.CreateMap<EditAccount, UserBase>().ReverseMap();

                cfg.CreateMap<IAnnouncement, MyAnnouncement>().IncludeAllDerived();

                cfg.CreateMap<ShowUser, UserBase>().ReverseMap();
                cfg.CreateMap<EditUser, UserBase>().ReverseMap();
                cfg.CreateMap<FilterUser, UserBase>();

                cfg.CreateMap<MyVacancy, VacancyBase>();
                cfg.CreateMap<VacancyBase, MyVacancy>()
                .ForMember(x => x.OwnerLogin, y => y.MapFrom(z => z.Creator.Login))
                .ForMember(x => x.EditorLogin, y => y.MapFrom(z => z.LastEditor.Login));

                cfg.CreateMap<MyCandidate, CandidateBase>();
                cfg.CreateMap<CandidateBase, MyCandidate>()
                .ForMember(x => x.OwnerLogin, y => y.MapFrom(z => z.Creator.Login))
                .ForMember(x => x.EditorLogin, y => y.MapFrom(z => z.LastEditor.Login));
            });

            return config;
        }
    }
}
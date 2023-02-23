using AutoMapper;
using InnoGotchi.Application.Common.Models;
using InnoGotchi.Application.Common.Models.BodyPartsModels;
using InnoGotchi.Domain.Common;
using InnoGotchi.Domain.Common.BodyParts;

namespace InnoGotchi.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Player, UserModel>()
            .ReverseMap();

        CreateMap<Farm, FarmViewModel>();

        CreateMap<CreateUpdateFarmModel, Farm>();

        //CreateMap<CreateUpdatePetModel, Pet>();
        //    .ForMember(
        //        pet => pet.Body.BodyId,
        //        opt
        //            => opt.MapFrom((src, dest) => src.BodyId))
        //    .ForMember(
        //        pet => pet.Body.NoseId,
        //        opt
        //            => opt.MapFrom((src, dest) => src.NoseId))
        //    .ForMember(
        //        pet => pet.Body.EyesId,
        //        opt
        //            => opt.MapFrom((src, dest) => src.EyesId))
        //    .ForMember(
        //        pet => pet.Body.MouthId,
        //        opt
        //            => opt.MapFrom((src, dest) => src.MouthId));

        CreateMap<Pet, PetViewModel>();

        CreateMap<Body, BodyModel>()
            .ReverseMap();

        CreateMap<Mouth, MouthModel>()
            .ReverseMap();

        CreateMap<Eyes, EyesModel>()
            .ReverseMap();

        CreateMap<Nose, NoseModel>()
            .ReverseMap();
    }
}

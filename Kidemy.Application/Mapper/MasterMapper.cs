using Kidemy.Application.ViewModels.CourseRequest;
using Kidemy.Application.ViewModels.Master;
using Kidemy.Domain.DTOs;
using Kidemy.Domain.Models.CourseRequest;
using Kidemy.Domain.Models.Master;
using System.Linq.Expressions;

namespace Kidemy.Application.Mapper
{
    public static class MasterMapper
    {
        public static ClientSideUpdateMasterViewModel MapFrom(this ClientSideUpdateMasterViewModel model, Master master)
        {
            model.Id = master.Id;
            model.Bio = master.Bio;
            model.FatherName = master.FatherName;
            model.NationalIDNumber = master.NationalIDNumber;
            model.IdentificationNumber = master.IdentificationNumber;
            model.PlaceOfIssuance = master.PlaceOfIssuance;
            model.PostalCode = master.PostalCode;
            model.Address = master.Address;
            model.IdentificationFileName = master.IdentificationFileName;
            model.Status = master.Status;

            return model;
        }

        public static Expression<Func<Master, AdminSideMasterViewModel>> AdminMapMasterViewModel => (Master master) => new AdminSideMasterViewModel
        {
            Id = master.Id,
            FatherName = master.FatherName,
            Bio = master.Bio,
            Address = master.Address,
            NationalIDNumber = master.NationalIDNumber,
            PlaceOfIssuance = master.PlaceOfIssuance,
            PostalCode = master.PostalCode,
            IdentificationFileName = master.IdentificationFileName,
            Status = master.Status,
            IdentificationNumber = master.IdentificationNumber,
            FirstName = master.User.FirstName,
            LastName = master.User.LastName,
            Mobile = master.User.Mobile,
            Email = master.User.Email

        };

        public static AdminSideMasterDetailsViewModel MapFrom(this AdminSideMasterDetailsViewModel model, Master master)
        {
            model.Id = master.Id;
            model.Bio = master.Bio;
            model.FatherName = master.FatherName;
            model.NationalIDNumber = master.NationalIDNumber;
            model.IdentificationNumber = master.IdentificationNumber;
            model.PlaceOfIssuance = master.PlaceOfIssuance;
            model.PostalCode = master.PostalCode;
            model.Address = master.Address;
            model.IdentificationFileName = master.IdentificationFileName;
            model.Status = master.Status;

            return model;
        }

        public static Expression<Func<Master, ClientSideMasterViewModel>> ClientMapMasterViewModel => (Master master) => new ClientSideMasterViewModel
        {
            Id = master.Id,
            FirstName = master.User.FirstName,
            LastName = master.User.LastName,
            AvatarName = master.User.AvatarName,
            Bio = master.Bio

        };

        public static ClientSideMasterDetailsViewModel MapFrom(this ClientSideMasterDetailsViewModel model, Master master)
        {
            model.Id = master.Id;
            model.FirstName = master.User.FirstName;
            model.LastName = master.User.LastName;
            model.Mobile = master.User.Mobile;
            model.Email = master.User.Email;
            model.AvatarName = master.User.AvatarName;
            model.Bio = master.Bio;
            model.Address = master.Address;

            return model;
        }
    }
}

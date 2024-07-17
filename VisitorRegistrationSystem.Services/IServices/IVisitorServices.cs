using VisitorRegistrationSystem.Common.Utility.Results.Abstract;
using VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs;

namespace VisitorRegistrationSystem.Services.IServices
{
    public interface IVisitorService
    {
        Task<IDataResult<VisitorDto>> Get(int visitorId);
        Task<IDataResult<int>> GetAllCount();
        Task<IDataResult<int>> GetNotIsExit();
        Task<IDataResult<int>> GetIsExit();
        Task<IDataResult<VisitorDto>> IsExit(int visitorId, string modifieldByName);

        Task<IDataResult<VisitorListDto>> GetAllNonDeleted();
        Task<IDataResult<VisitorListDto>> GetAllNonDeletedAndActive();

        Task<IDataResult<VisitorDto>> Add(VisitorAddDto visitorAddDto, string createdByName);



        Task<IDataResult<VisitorDto>> Delete(int visitorId, string modifieldByName);
        Task<IResult> HardDelete(int visitorId);
        Task<IDataResult<VisitorDto>> Update(VisitorUpdateDto visitorUpdateDto, string modifiedByName);


    }
}

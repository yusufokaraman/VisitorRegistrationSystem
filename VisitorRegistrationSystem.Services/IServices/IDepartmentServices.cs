using VisitorRegistrationSystem.Common.Utility.Results.Abstract;
using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;

namespace VisitorRegistrationSystem.Services.IServices
{
    public interface IDepartmentService
    {
        Task<IDataResult<DepartmentDto>> Get(int departmentId);
        Task<IDataResult<DepartmentUpdateDto>> GetDepartmentUpdateDto(int departmentId);
        Task<IDataResult<DepartmentListDto>> GetAll();
        Task<IDataResult<DepartmentListDto>> GetAllNonDeleted();
        Task<IDataResult<DepartmentListDto>> GetAllNonDeletedAndActive();

        Task<IDataResult<DepartmentDto>> Add(DepartmentAddDto departmentAddDto, string createdByName);

        Task<IDataResult<DepartmentDto>> Update(DepartmentUpdateDto departmentUpdateDto, string modifieldByName);

        Task<IDataResult<DepartmentDto>> Delete(int departmentId, string modifieldByName);
        Task<IResult> HardDelete(int departmentId);

    }
}

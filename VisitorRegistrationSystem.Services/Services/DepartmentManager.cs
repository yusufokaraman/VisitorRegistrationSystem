using AutoMapper;
using VisitorRegistrationSystem.Common.Utility.Results.Abstract;
using VisitorRegistrationSystem.Common.Utility.Results.Concrete;
using VisitorRegistrationSystem.Common.Utility.Results.Types;
using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;
using VisitorRegistrationSystem.Domain.Entitiy;
using VisitorRegistrationSystem.Repository.IRepository;
using VisitorRegistrationSystem.Services.IServices;

namespace VisitorRegistrationSystem.Services.Services
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<DepartmentDto>> Add(DepartmentAddDto departmentAddDto, string createdByName)
        {
            var department = _mapper.Map<Department>(departmentAddDto);
            department.CreatedByName = createdByName;
            department.ModifiedByName = createdByName;

            var addedDepartment = await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.SaveAsync();
            return new DataResult<DepartmentDto>(ResultStatus.Success, new DepartmentDto()
            {
                Department = addedDepartment,
                ResultStatus = ResultStatus.Success,
                Message = $"{department.Name} birim kaydı  başarıyla eklenmiştir."


            }, $"{department.Name} birim kaydı  başarıyla eklenmiştir.");

        }

        public async Task<IDataResult<DepartmentDto>> Delete(int departmentId, string modifieldByName)
        {
            //Soft delete
            var result = await _unitOfWork.Departments.AnyAsync(c => c.Id == departmentId);
            if (result)
            {
                var department = await _unitOfWork.Departments.GetAsync(c => c.Id == departmentId);
                department.IsDeleted = true;
                department.ModifiedByName = modifieldByName;
                department.ModifiedDate = DateTime.Now;
                var deletedDepartment = await _unitOfWork.Departments.UpdateAsync(department);
                await _unitOfWork.SaveAsync();
                return new DataResult<DepartmentDto>(ResultStatus.Success, new DepartmentDto()
                {

                    Department = deletedDepartment,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedDepartment.Name} birim kaydı  silinmiştir."
                }, $"{deletedDepartment.Name} birim kaydı  silinmiştir.");

            }
            return new DataResult<DepartmentDto>(ResultStatus.Error, new DepartmentDto()
            {
                Department = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir birim kaydı bulunamamışır."

            }, "Böyle bir birim kaydı bulunamamışır.");
        }

        public async Task<IDataResult<DepartmentDto>> Get(int departmentId)
        {
            var department = await _unitOfWork.Departments.GetAsync(d => d.Id == departmentId);

            if (department != null)
            {
                return new DataResult<DepartmentDto>(ResultStatus.Success, new DepartmentDto()
                {
                    Department = department,
                    ResultStatus = ResultStatus.Success,


                });



            }
            return new DataResult<DepartmentDto>(ResultStatus.Error, new DepartmentDto()
            {
                Department = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir birim bulunamadı."


            }, "Böyle bir birim bulunamadı.");
        }

        public async Task<IDataResult<DepartmentListDto>> GetAll()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync(null);
            if (departments.Count > 0)
            {
                return new DataResult<DepartmentListDto>(ResultStatus.Success, new DepartmentListDto()
                {
                    Departments = (List<Department>)departments,
                    ResultStatus = ResultStatus.Success


                });
            }
            return new DataResult<DepartmentListDto>(ResultStatus.Error, new DepartmentListDto()
            {
                Departments = null,
                ResultStatus = ResultStatus.Error,
                Message = "Birim kaydı  bulunamadı."

            }, "Birim kaydı bulunamadı.");
        }

        public async Task<IDataResult<DepartmentListDto>> GetAllNonDeleted()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync(d => d.IsDeleted == false);
            if (departments.Count > 0)
            {
                return new DataResult<DepartmentListDto>(ResultStatus.Success, new DepartmentListDto()
                {
                    Departments = (List<Department>)departments,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<DepartmentListDto>(ResultStatus.Error, new DepartmentListDto()
            {
                Departments = null,
                ResultStatus = ResultStatus.Error,
                Message = "Birim kaydı bulunamadı."

            }, "Birim kaydı bulunamadı.");
        }

        public async Task<IDataResult<DepartmentListDto>> GetAllNonDeletedAndActive()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync(d => d.IsDeleted == false && d.IsActive == true);
            if (departments.Count > 0)
            {
                return new DataResult<DepartmentListDto>(ResultStatus.Success, new DepartmentListDto()
                {
                    Departments = (List<Department>)departments,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<DepartmentListDto>(ResultStatus.Error, new DepartmentListDto()
            {
                Departments = null,
                ResultStatus = ResultStatus.Error,
                Message = "Birim kaydı bulunamadı."

            }, "Birim kaydı bulunamadı.");

        }

        public async Task<IDataResult<DepartmentUpdateDto>> GetDepartmentUpdateDto(int departmentId)
        {
            var result = await _unitOfWork.Departments.AnyAsync(d => d.Id == departmentId);
            if (result)
            {
                var department = await _unitOfWork.Departments.GetAsync(d => d.Id == departmentId);
                var departmentDto = _mapper.Map<DepartmentUpdateDto>(department);
                return new DataResult<DepartmentUpdateDto>(ResultStatus.Success, departmentDto, "Başarıyla güncellendi.");

            }
            return new DataResult<DepartmentUpdateDto>(ResultStatus.Error, null, "Böyle bir birim bulunamadı.");
        }

        public async Task<IResult> HardDelete(int departmentId)
        {
            var result = await _unitOfWork.Departments.AnyAsync(c => c.Id == departmentId);
            if (result)
            {
                var department = await _unitOfWork.Departments.GetAsync(c => c.Id == departmentId);

                await _unitOfWork.Departments.DeleteAsync(department);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{department.Name} birim  veritabanından silinmiştir.");

            }
            return new Result(ResultStatus.Error, "Böyle bir birim bulunamamışır.");
        }

        public async Task<IDataResult<DepartmentDto>> Update(DepartmentUpdateDto departmentUpdateDto, string modifieldByName)
        {
            var oldDepartment = await _unitOfWork.Departments.GetAsync(d => d.Id == departmentUpdateDto.Id);

            var department = _mapper.Map<DepartmentUpdateDto, Department>(departmentUpdateDto, oldDepartment);
            department.ModifiedByName = modifieldByName;


            var updatedDeparment = await _unitOfWork.Departments.UpdateAsync(department);
            await _unitOfWork.SaveAsync();
            return new DataResult<DepartmentDto>(ResultStatus.Success, new DepartmentDto()
            {
                Department = updatedDeparment,
                ResultStatus = ResultStatus.Success,
                Message = $"{department.Name} birim kaydı güncellendi."


            }, $"{department.Name} birim kaydı güncellendi.");
        }
    }
}

﻿using AutoMapper;
using TurkishId;
using VisitorRegistrationSystem.Common.Utility.Results.Abstract;
using VisitorRegistrationSystem.Common.Utility.Results.Concrete;
using VisitorRegistrationSystem.Common.Utility.Results.Types;
using VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs;
using VisitorRegistrationSystem.Domain.Entity;
using VisitorRegistrationSystem.Repository.IRepository;
using VisitorRegistrationSystem.Services.IServices;

namespace VisitorRegistrationSystem.Services.Services
{
    public class VisitorManager : IVisitorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VisitorManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<VisitorDto>> Add(VisitorAddDto visitorAddDto, string createdByName)
        {
            // T.C. Kimlik No doğrulama
            var isValid = TurkishIdNumber.IsValid(visitorAddDto.TcNo.ToString());

            if (!isValid)
                return new DataResult<VisitorDto>(ResultStatus.Error, null, "Geçersiz T.C. Kimlik No");

            var visitor = _mapper.Map<Visitor>(visitorAddDto);
            visitor.CreatedByName = createdByName;
            visitor.ModifiedByName = createdByName;
            visitor.EnterDate = DateTime.Now;

            var addedVisitor = await _unitOfWork.Visitors.AddAsync(visitor);
            await _unitOfWork.SaveAsync();
            return new DataResult<VisitorDto>(ResultStatus.Success, new VisitorDto()
            {
                Visitor = addedVisitor,
                ResultStatus = ResultStatus.Success,
                Message = $"{visitor.FirstName + visitor.LastName} ziyaretçi kaydı  başarıyla eklenmiştir."


            }, $"{visitor.FirstName + visitor.LastName} ziyaretçi kaydı  başarıyla eklenmiştir.");

        }

        public async Task<IDataResult<VisitorDto>> Delete(int visitorId, string modifieldByName)
        {
            //Soft Delete
            var result = await _unitOfWork.Visitors.AnyAsync(c => c.Id == visitorId);
            if (result)
            {
                var visitor = await _unitOfWork.Visitors.GetAsync(c => c.Id == visitorId);
                visitor.IsDeleted = true;
                visitor.ModifiedByName = modifieldByName;
                visitor.ModifiedDate = DateTime.Now;
                var deletedVisitor = await _unitOfWork.Visitors.UpdateAsync(visitor);
                await _unitOfWork.SaveAsync();
                return new DataResult<VisitorDto>(ResultStatus.Success, new VisitorDto()
                {

                    Visitor = deletedVisitor,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{visitor.FirstName + visitor.LastName} ziyaretçi kaydı  başarıyla silinmiştir."
                }, $"{visitor.FirstName + visitor.LastName} ziyaretçi kaydı  başarıyla silinmiştir.");

            }
            return new DataResult<VisitorDto>(ResultStatus.Error, new VisitorDto()
            {
                Visitor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir ziyaretçi kaydı bulunamamışır."

            }, "Böyle bir ziyaretçi kaydı bulunamamışır.");
        }

        public async Task<IDataResult<VisitorDto>> Get(int visitorId)
        {
            var visitor = await _unitOfWork.Visitors.GetAsync(d => d.Id == visitorId);

            if (visitor != null)
            {
                return new DataResult<VisitorDto>(ResultStatus.Success, new VisitorDto()
                {
                    Visitor = visitor,
                    ResultStatus = ResultStatus.Success,


                });



            }
            return new DataResult<VisitorDto>(ResultStatus.Error, new VisitorDto()
            {
                Visitor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir ziyaretçi kaydı bulunamadı."


            }, "Böyle bir ziyaretçi kaydı bulunamadı.");
        }

        public async Task<IDataResult<VisitorListDto>> GetAll()
        {
            var visitors = await _unitOfWork.Visitors.GetAllAsync(null);
            if (visitors.Count > 0)
            {

                return new DataResult<VisitorListDto>(ResultStatus.Success, new VisitorListDto()
                {

                    Visitors = (List<Visitor>)visitors,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<VisitorListDto>(ResultStatus.Error, new VisitorListDto()
            {
                Visitors = null,
                ResultStatus = ResultStatus.Error,
                Message = "Ziyaretçi kaydı  bulunamadı."

            }, "Ziyaretçi kaydı bulunamadı.");
        }

        public async Task<IDataResult<int>> GetAllCount()
        {
            var count = await _unitOfWork.Visitors.CountAsync(v => v.IsActive);
            if (count > 0)
            {
                return new DataResult<int>(ResultStatus.Success, count);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, -1, message: "Beklenmeyen bir hata ile karşılaşıldı");
            }



        }

        public async Task<IDataResult<VisitorListDto>> GetAllNonDeleted()
        {
            var visitors = await _unitOfWork.Visitors.GetAllAsync(d => d.IsDeleted == false, x => x.Department);
            if (visitors.Count > 0)
            {
                return new DataResult<VisitorListDto>(ResultStatus.Success, new VisitorListDto()
                {
                    Visitors = (List<Visitor>)visitors,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<VisitorListDto>(ResultStatus.Error, new VisitorListDto()
            {
                Visitors = null,
                ResultStatus = ResultStatus.Error,
                Message = "Ziyaretçi kaydı bulunamadı."

            }, "Ziyaretçi kaydı bulunamadı.");
        }

        public async Task<IDataResult<VisitorListDto>> GetAllNonDeletedAndActive()
        {
            var visitors = await _unitOfWork.Visitors.GetAllAsync(d => d.IsDeleted == false && d.IsActive == true);
            if (visitors.Count > 0)
            {
                return new DataResult<VisitorListDto>(ResultStatus.Success, new VisitorListDto()
                {
                    Visitors = (List<Visitor>)visitors,
                    ResultStatus = ResultStatus.Success

                });
            }
            return new DataResult<VisitorListDto>(ResultStatus.Error, new VisitorListDto()
            {
                Visitors = null,
                ResultStatus = ResultStatus.Error,
                Message = "Ziyaretçi  kaydı bulunamadı."

            }, "Ziyaretçi kaydı bulunamadı.");
        }

        public async Task<IDataResult<int>> GetIsExit()
        {
            var count = await _unitOfWork.Visitors.CountAsync(v => v.IsExit == true);
            if (count > 0)
            {
                return new DataResult<int>(ResultStatus.Success, count);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, 0, message: "Beklenmeyen bir hata ile karşılaşıldı");
            }
        }

        public async Task<IDataResult<int>> GetNotIsExit()
        {
            var count = await _unitOfWork.Visitors.CountAsync(v => v.IsExit == false);
            if (count > 0)
            {
                return new DataResult<int>(ResultStatus.Success, count);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, 0, message: "Beklenmeyen bir hata ile karşılaşıldı");
            }
        }

        public async Task<IResult> HardDelete(int visitorId)
        {
            var result = await _unitOfWork.Visitors.AnyAsync(c => c.Id == visitorId);
            if (result)
            {
                var visitor = await _unitOfWork.Visitors.GetAsync(c => c.Id == visitorId);

                await _unitOfWork.Visitors.DeleteAsync(visitor);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{visitor.FirstName} adlı ziyaretçi veritabanından silinmiştir.");

            }
            return new Result(ResultStatus.Error, " Ziyaretçi veritabanından silinmiştir.");
        }

        public async Task<IDataResult<VisitorDto>> IsExit(int visitorId, string modifieldByName)
        {

            var result = await _unitOfWork.Visitors.AnyAsync(c => c.Id == visitorId && c.IsExit == false);
            if (result)
            {
                var visitor = await _unitOfWork.Visitors.GetAsync(c => c.Id == visitorId);
                visitor.IsExit = true;
                visitor.OutDate = DateTime.Now;
                visitor.ModifiedByName = modifieldByName;
                visitor.ModifiedDate = DateTime.Now;
                var IsExitVisitor = await _unitOfWork.Visitors.UpdateAsync(visitor);
                await _unitOfWork.SaveAsync();
                return new DataResult<VisitorDto>(ResultStatus.Success, new VisitorDto()
                {

                    Visitor = IsExitVisitor,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{visitor.FirstName + visitor.LastName} ziyaretçi çıkış yapmıştır."
                }, $"{visitor.FirstName + visitor.LastName} ziyaretçi çıkış yapmıştır.");

            }
            return new DataResult<VisitorDto>(ResultStatus.Error, new VisitorDto()
            {
                Visitor = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir ziyaretçi kaydı bulunamamışır."

            }, "Böyle bir ziyaretçi kaydı bulunamamışır.");
        }

        public async Task<IDataResult<VisitorDto>> Update(VisitorUpdateDto visitorUpdateDto, string modifiedByName)
        {
            var visitor = await _unitOfWork.Visitors.GetAsync(c => c.Id == visitorUpdateDto.Id);
            if (visitor == null)
            {
                return new DataResult<VisitorDto>(ResultStatus.Error, new VisitorDto()
                {
                    Visitor = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Böyle bir ziyaretçi kaydı bulunamamışır."
                }, "Böyle bir ziyaretçi kaydı bulunamamışır.");
            }

            _mapper.Map(visitorUpdateDto, visitor);
            visitor.ModifiedByName = modifiedByName;
            visitor.ModifiedDate = DateTime.Now;

            var updatedVisitor = await _unitOfWork.Visitors.UpdateAsync(visitor);
            await _unitOfWork.SaveAsync();
            return new DataResult<VisitorDto>(ResultStatus.Success, new VisitorDto()
            {
                Visitor = updatedVisitor,
                ResultStatus = ResultStatus.Success,
                Message = $"{visitor.FirstName + visitor.LastName} ziyaretçi kaydı başarıyla güncellenmiştir."
            }, $"{visitor.FirstName + visitor.LastName} ziyaretçi kaydı başarıyla güncellenmiştir.");
        }
    }
}

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using VisitorRegistrationSystem.Common.Utility.Results.Types;
using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;
using VisitorRegistrationSystem.Domain.Entitiy;
using VisitorRegistrationSystem.Services.IServices;
using VisitorRegistrationSystem.UI.Models;
using VisitorRegistrationSystem.Common.Utility.Extensions;

namespace VisitorRegistrationSystem.UI.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly UserManager<User> _userManager;
        public DepartmentController(IDepartmentService departmentService, UserManager<User> userManager)
        {
            _departmentService = departmentService;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _departmentService.GetAllNonDeleted();

            return View(result.Data);


        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_DepartmentAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentAddDto departmentAddDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var result = await _departmentService.Add(departmentAddDto, user.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var departmentAddAjaxModel = JsonSerializer.Serialize(new DepartmentAddAjaxViewModel
                    {
                        DepartmentDto = result.Data,
                        DepartmentAddPartial = await this.RenderViewToStringAsync("_DepartmentAddPartial", departmentAddDto)
                    });
                    return Json(departmentAddAjaxModel);
                }
            }
            var departmentAddAjaxErrorModel = JsonSerializer.Serialize(new DepartmentAddAjaxViewModel
            {
                DepartmentAddPartial = await this.RenderViewToStringAsync("_DepartmentAddPartial", departmentAddDto)
            });
            return Json(departmentAddAjaxErrorModel);


        }

        [HttpGet]
        public async Task<IActionResult> Update(int departmentId)
        {
            var result = await _departmentService.GetDepartmentUpdateDto(departmentId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_DepartmentUpdatePartial", result.Data);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentUpdateDto departmentUpdateDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (ModelState.IsValid)
            {
                var result = await _departmentService.Update(departmentUpdateDto, user.UserName);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var departmentUpdateAjaxModel = JsonSerializer.Serialize(new DepartmentUpdateAjaxViewModel
                    {
                        DepartmentDto = result.Data,
                        DepartmentUpdatePartial = await this.RenderViewToStringAsync("_DepartmentUpdatePartial", departmentUpdateDto)
                    });
                    return Json(departmentUpdateAjaxModel);
                }
            }
            var departmentUpdateAjaxErrorModel = JsonSerializer.Serialize(new DepartmentUpdateAjaxViewModel
            {
                DepartmentUpdatePartial = await this.RenderViewToStringAsync("_DepartmentUpdatePartial", departmentUpdateDto)
            });
            return Json(departmentUpdateAjaxErrorModel);


        }


        public async Task<JsonResult> GetAllDepartments()
        {
            var result = await _departmentService.GetAllNonDeleted();
            var departments = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve

            });
            return Json(departments);

        }

        [HttpPost]
        public async Task<JsonResult> Delete(int departmentId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var result = await _departmentService.Delete(departmentId, user.UserName);
            var deletedDepartment = JsonSerializer.Serialize(result.Data);
            return Json(deletedDepartment);

        }



    }

}

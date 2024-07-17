using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VisitorRegistrationSystem.Common.Utility.Extensions;
using VisitorRegistrationSystem.Common.Utility.Results.Types;
using VisitorRegistrationSystem.Domain.DTOs.UserDTOs;
using VisitorRegistrationSystem.Domain.Entitiy;
using Microsoft.EntityFrameworkCore;

namespace VisitorRegistrationSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, IWebHostEnvironment env, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _mapper = mapper;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(new UserListDto()
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return Ok(new { Message = "Login successful" });
                    }
                    else
                    {
                        return Unauthorized("E-Posta adresiniz veya Şifreniz hatalı!");
                    }
                }
                else
                {
                    return Unauthorized("E-Posta adresiniz veya Şifreniz hatalı!");
                }
            }
            return BadRequest("Invalid login attempt");
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromForm] UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName, userAddDto.PictureFile);
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password);
                if (result.Succeeded)
                {
                    return Ok(new UserDto()
                    {
                        ResultStatus = ResultStatus.Success,
                        Message = $"{userAddDto.UserName} adına sahip kullanıcı, başarıyla eklenmiştir.",
                        User = user
                    });
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            if (id != userUpdateDto.Id)
            {
                return BadRequest("ID mismatch");
            }

            var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
            if (oldUser == null)
            {
                return NotFound("User not found");
            }

            if (ModelState.IsValid)
            {
                bool imgIsTrue = false;
                var oldPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    imgIsTrue = true;
                }
                var updateUser = _mapper.Map(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updateUser);
                if (result.Succeeded)
                {
                    if (imgIsTrue && oldPicture != "DefaultUser.png")
                    {
                        ImageDelete(oldPicture);
                    }
                    return NoContent();
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new UserDto()
                {
                    ResultStatus = ResultStatus.Success,
                    Message = $"{user.UserName} adına sahip kullanıcı, başarıyla silinmiştir.",
                    User = user
                });
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return Ok(userUpdateDto);
        }

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword, userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        return Ok("Password changed successfully");
                    }
                    return BadRequest(result.Errors);
                }
                return Unauthorized("Current password is incorrect");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("ChangeDetails")]
        [Authorize]
        public async Task<IActionResult> ChangeDetails([FromBody] UserUpdateDto userUpdateDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (ModelState.IsValid)
            {
                bool imgIsTrue = false;
                var oldPicture = user.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    if (oldPicture != "DefaultUser.png")
                    {
                        imgIsTrue = true;
                    }
                }
                var updateUser = _mapper.Map(userUpdateDto, user);
                var result = await _userManager.UpdateAsync(updateUser);
                if (result.Succeeded)
                {
                    if (imgIsTrue)
                    {
                        ImageDelete(oldPicture);
                    }
                    return Ok($"{updateUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                }
                return BadRequest(result.Errors);
            }
            return BadRequest(ModelState);
        }

        [HttpPost("ImageUpload")]
        [Authorize(Roles = "Admin,Personal")]
        public async Task<IActionResult> UserImageUpload([FromQuery] string userName, [FromForm] IFormFile pictureFile)
        {
            string fileName = await ImageUpload(userName, pictureFile);
            return Ok(fileName);
        }

        [HttpDelete("ImageDelete")]
        [Authorize(Roles = "Admin,Personal")]
        public IActionResult UserImageDelete([FromQuery] string pictureName)
        {
            bool isDeleted = ImageDelete(pictureName);
            if (isDeleted)
            {
                return Ok("Image deleted successfully");
            }
            return NotFound("Image not found");
        }

        private async Task<string> ImageUpload(string userName, IFormFile pictureFile)
        {
            string wwwroot = _env.WebRootPath;
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            var dateTime = DateTime.Now;
            string fileName = $"{userName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return fileName;
        }

        private bool ImageDelete(string pictureName)
        {
            string wwwroot = _env.WebRootPath;
            var fileToDelete = Path.Combine($"{wwwroot}/img", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            return false;
        }
    }
}

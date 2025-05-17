using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TransVault.Application.Interfaces;
using TransVault.Common;

namespace TransVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : BaseApiController
    {
        private readonly IUserService _UserService;
        private IHttpContextAccessor _httpContext;
        public UsersController(IUserService UserService, IHttpContextAccessor httpContext)
        {
            _UserService = UserService;
            _httpContext = httpContext;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {

                var res = _httpContext?.HttpContext?.User.Claims.Single(x => x.Type == "username").Value;
                var Users = await _UserService.GetAll(res ?? "");
                _response.Result = Users;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        // GET: api/Users/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIResponse>> GetUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    return Ok(_response.InvalidPropertyResponse("Id"));
                }
                var User = await _UserService.GetUser(id);
                if (User == null)
                {
                    return Ok(_response.InvalidPropertyResponse("Id"));
                }
                _response.Result = User;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        // PUT: api/Users/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<APIResponse>> UpdateUser(int id, [FromBody] UserDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id == 0)
                {

                    return Ok(_response.InvalidData());
                }
                var User = await _UserService.GetUserByEmail(updateDTO.Email);
                if (User != null && User.Id != id)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "email of User already Exists!" };
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return _response;
                }


                await _UserService.UpdateUser(updateDTO);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;

                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateUser([FromBody] UserDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return Ok(_response.InvalidData());
                }
                var res = await _UserService.IsEmailExist(createDTO);
                if (res)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "email of User already Exists!" };
                    return Ok(_response);
                }

                var username = _httpContext?.HttpContext?.User.Claims.Single(x => x.Type == "username").Value;
                createDTO.Createdby = username;

                await _UserService.AddUser(createDTO);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = createDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;

                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }







        // DELETE: api/Users/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteUser(int id)
        {
            try
            {
                if (id == 0 || _UserService.GetUser(id).Result == null)
                {
                    return Ok(_response.InvalidPropertyResponse("Id"));
                }
                await _UserService.DeleteUser(id);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }
}

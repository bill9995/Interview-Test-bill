using Interview_Test.Models;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Interview_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("GetUsers")]
    public ActionResult GetUsers()
    {
        var users = _userRepository.GetUsers();
        return Ok(users);
    }

    [HttpGet("GetUserById/{id}")]
    public ActionResult GetUserById(string id)
    {
        var user = _userRepository.GetUserById(id);
        return Ok(user);
    }
    
    [HttpPost("CreateUser")]
    public ActionResult CreateUser()
    {
        int affectedRows = _userRepository.CreateUser();
        return Ok(new { affectedRows });
    }
}
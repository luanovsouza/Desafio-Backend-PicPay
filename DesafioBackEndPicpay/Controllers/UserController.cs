using DesafioBackEndPicpay.Models;
using DesafioBackEndPicpay.Repositories.Interfaces;
using DesafioBackEndPicpay.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioBackEndPicpay.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IUserService _userService;
    private readonly IRepository<User> _repository;

    public UserController(IUnitOfWork uof, IUserService userService, IRepository<User> repository)
    {
        _uof = uof;
        _userService = userService;
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<User> GetUser()
    {
        var users = _repository.GetAll();
        
        if(users != Empty)
            return Ok(users);
        
        return NotFound("Users not found");
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var userById = _repository.GetById(us => us.Id == id);

        if (userById != null)
            return Ok(userById);
        
        return NotFound("User not found");
    }

    [HttpPost("CreateUser", Name = "CreateUser")]
    public async Task<ActionResult<User>> PostUser(User user)
    {
      
        if (user == null) 
            return BadRequest("Data incorret!");
        
        var emailExists =  await _repository.GetById(u => u.Email == user.Email);
        if (emailExists != null)
            return Conflict("Email already in use!");

        var documentExists =  await _repository.GetById(u => u.Documento == user.Documento);
        if (documentExists != null)
            return Conflict("Document already in use!");
        
        _repository.Create(user);
        await _uof.CommitAsync();
            
        return new CreatedAtRouteResult("CreateUser", new { id = user.Id }, user);
    }
}
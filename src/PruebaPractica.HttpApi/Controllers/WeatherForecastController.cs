using Microsoft.AspNetCore.Mvc;
using PruebaPractica.HttpApi.Model.UserModel;
using System;
using System.Text;

namespace PruebaPractica.HttpApi.Controllers;

[ApiController]
[Route("[controller]")]

public class WeatherForecastController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        this.logger = logger;
    }

    private static List<UserModel> _userList = new List<UserModel>{
                new  UserModel{identification="1700000001", name="Pedro"},
                new  UserModel{identification="1700000002", name="Juan"}
            };

    [HttpGet(Name = "{search}")]

    public UserModel GetUser([FromHeader] string search)
    {

        logger.LogInformation("Buscando usuario por identificacion");

        var user = _userList.Where(x => x.identification == search).SingleOrDefault();

        if (user != null)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(user.identification);
            var identificationBase64 = System.Convert.ToBase64String(plainTextBytes);

            return new UserModel
            {
                name = user.name,
                identification = identificationBase64
            };
        }

        logger.LogWarning($"La cedula con: {search} no ha sido encontrada");

        throw new Exception("Error en la busqueda en la cedula");

    }
}

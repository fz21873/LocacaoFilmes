using BAL.RuleBusiness;
using LocadoraFilmes.API.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace LocadoraFilmes.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerGenerico<Director,long>
    {

        public DirectorController(DirectorRuleBusiness directorRuleBusiness): base(directorRuleBusiness)
        {

        }
    }
}

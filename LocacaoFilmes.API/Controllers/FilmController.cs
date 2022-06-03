using BAL.RuleBusiness;
using LocadoraFilmes.API.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace LocadoraFilmes.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerGenerico<Film,long>
    {
        
        public FilmController(FilmRuleBusiness filmRuleBusiness) : base(filmRuleBusiness)
        {
            
        }
       
    }
}

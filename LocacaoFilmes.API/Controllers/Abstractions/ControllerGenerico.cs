using BAL.RuleBusiness.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Threading.Tasks;

namespace LocadoraFilmes.API.Controllers.Abstractions
{
    
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControllerGenerico<TClasse,TChave> : ControllerBase where TClasse : EntityBase
    {
        private DefaultRuleBusiness<TClasse> _defaultRuleBusiness;

        public ControllerGenerico(DefaultRuleBusiness<TClasse> defaultRuleBusiness)
        {
            _defaultRuleBusiness = defaultRuleBusiness;
        }



        [HttpGet]
        public virtual async Task<IActionResult> GetAll()

        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entity = await _defaultRuleBusiness.FindAll();
                if (entity == null)
                {
                    return NotFound();
                }

                return Ok(entity);

            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetFindById([FromRoute] TChave id)

        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var entidade = await _defaultRuleBusiness.FindById(id);
                if (entidade == null)
                {
                    return NotFound();
                }

                return Ok(entidade);

            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public virtual async Task<IActionResult> CreateEntity([FromBody] TClasse model)
        {
            try
            {
                await _defaultRuleBusiness.ValidateAsync(model, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _defaultRuleBusiness.Create(model);

                return StatusCode(StatusCodes.Status201Created, model);
            }
            catch (Exception ex)
            {
               
                Console.WriteLine(value: ex.InnerException.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> UpdateEntity([FromRoute] TChave id, [FromBody] TClasse entity)
        {
            try
            {
                await _defaultRuleBusiness.ValidateAsync(entity, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _defaultRuleBusiness.Update(entity))
                {
                    return NoContent();
                }
                else
                {
                  

                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity([FromRoute] TChave id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var model = await _defaultRuleBusiness.FindById(id);
                if (model == null)
                {
                    return NotFound();
                }
                await _defaultRuleBusiness.Delete(model);

                return Ok(model);
            }
            catch (Exception ex)
            {
                

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


       
    }
}

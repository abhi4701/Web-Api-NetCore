using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Data;
using Web_Api.Models;
using Web_Api.Models.DTO;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult< IEnumerable<VillaDTO>> GetVilas()
        {
            return Ok(VillaStore.villaList); 
        }

        [HttpGet("id:int",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVila(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
             if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if(villaDTO.Id > 0) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(x=>x.Id).FirstOrDefault().Id+1;
            VillaStore.villaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new {id=villaDTO.Id },villaDTO);
        }

        [HttpDelete("id:int", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if ( id==0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
           
        }

        [HttpPut("id:int", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id , [FromBody] VillaDTO villadto)
        {
            if(villadto == null || id != villadto.Id)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            villa.Name= villadto.Name;
            villa.Sqft = villadto.Sqft;
            villa.Occupancy = villadto.Occupancy;

            return NoContent();
        }



        [HttpPatch("id:int", Name = "PatchPartialVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchdto)
        {
            if(patchdto==null || id == 0)
            {
                return BadRequest();
            }
            var villa= VillaStore.villaList.FirstOrDefault(x=>x.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            patchdto.ApplyTo(villa,ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent() ;
        }

    }
}

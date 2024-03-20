using MagicVillaAPICourse.Data;
using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVillaAPICourse.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        public VillaAPIController()
        {
        }


        #region Get All Villas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas ()
        {
            return Ok(VillaStore.villalist);
        }
        #endregion

        #region Get Villa

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
                return BadRequest();
            
                
            
            var villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            
            if (villa == null) 
                return NotFound();
            
            return Ok(villa);
        }

        #endregion

        #region Create Villa
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Villa> CreateVilla([FromBody]VillaDto villaDTO) 
        {
            if (VillaStore.villalist.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            { 
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
                return BadRequest(villaDTO);

            if (villaDTO.Id > 0)
                return StatusCode(StatusCodes.Status500InternalServerError);
            
            villaDTO.Id = VillaStore.villalist.OrderByDescending(u => u.Id).FirstOrDefault().Id+1;
            VillaStore.villalist.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id },  villaDTO);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            if (villa == null)
                return NotFound();

            VillaStore.villalist.Remove(villa);
            return NoContent();
        }
        #endregion

        #region Update
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
                return BadRequest();

            var villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            villa.Name = villaDto.Name;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villaDto.Occupancy;

            return NoContent();

        }
        #endregion

        #region Patch Villa Name
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> PatchDto)
        {
            if (PatchDto == null || id == 0)
                return BadRequest();

            var villa = VillaStore.villalist.FirstOrDefault(u => u.Id == id);
            if (villa == null) 
                return NotFound();

            PatchDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NoContent();

        }
        #endregion

    }
}

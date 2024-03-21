using MagicVillaAPICourse.Data;
using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPICourse.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public VillaAPIController(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Get All Villas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas ()
        {
            return Ok(_db.Villas);
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
            
                
            
            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            
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
        public ActionResult<Villa> CreateVilla([FromBody]VillaCreateDTO villaDto) 
        {
            if (_db.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDto.Name.ToLower()) != null)
            { 
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
                return BadRequest(villaDto);
            

            Villa model = new()
            {
                Amenity = villaDto.Amenity,
                Details = villaDto.Details,
                ImageUrl = villaDto.ImageUrl,
                Name = villaDto.Name,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
            };

            _db.Villas.Add(model);
            _db.SaveChanges();
            
            return CreatedAtRoute("GetVilla", new { id = model.Id },  model);
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

            var villa = _db.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
                return NotFound();

            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }
        #endregion

        #region Update
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody]VillaUpdateDTO villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
                return BadRequest();

            Villa model = new()
            {
                Amenity = villaDto.Amenity,
                Details = villaDto.Details,
                Id = villaDto.Id,
                ImageUrl = villaDto.ImageUrl,
                Name = villaDto.Name,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();

        }
        #endregion

        #region Patch Villa Name
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> PatchDto)
        {
            if (PatchDto == null || id == 0)
                return BadRequest();

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);

            if (villa == null) 
                return NotFound();

            VillaUpdateDTO villaDto = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
            };

            PatchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Villa model = new()
            {
                Amenity = villaDto.Amenity,
                Details = villaDto.Details,
                Id = villaDto.Id,
                ImageUrl = villaDto.ImageUrl,
                Name = villaDto.Name,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();

        }
        #endregion

    }
}

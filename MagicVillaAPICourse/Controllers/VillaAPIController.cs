using AutoMapper;
using MagicVillaAPICourse.Data;
using MagicVillaAPICourse.Models;
using MagicVillaAPICourse.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IMapper _mapper;
        public VillaAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        #region Get All Villas
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<List<VillaDto>>(villaList));
        }
        #endregion

        #region Get Villa

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);

            if (villa == null)
                return NotFound();

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        #endregion

        #region Create Villa
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Villa>> CreateVilla([FromBody] VillaCreateDTO createDto)
        {
            if (await _db.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == createDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa already exists!");
                return BadRequest(ModelState);
            }
            if (createDto == null)
                return BadRequest(createDto);

            Villa model = _mapper.Map<Villa>(createDto);

            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();

            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
                return NotFound();

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        #endregion

        #region Update
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
                return BadRequest();

            Villa model = _mapper.Map<Villa>(updateDto);

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();

        }
        #endregion

        #region Patch Villa Name
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> PatchDto)
        {
            if (PatchDto == null || id == 0)
                return BadRequest();

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (villa == null)
                return NotFound();

            
            VillaUpdateDTO villaDto = _mapper.Map<VillaUpdateDTO>(villa);

            PatchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Villa model = _mapper.Map<Villa>(villaDto);

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();

        }
        #endregion

    }
}

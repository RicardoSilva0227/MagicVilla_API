using AutoMapper;
using MagicVillaWebProject.Models;
using MagicVillaWebProject.Models.Dto;
using MagicVillaWebProject.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace MagicVillaWebProject.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        #region Index
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> list = new();

            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.result));
            }
            return View(list);
        }
        #endregion

        #region Create
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Villa Created Successfully!";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }
        #endregion

        #region Update
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.result));
                    return View(_mapper.Map<VillaUpdateDTO>(model));
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Villa Updated Successfully!";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        #endregion

        #region Delete
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(SD.SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(VillaDto model)
        {
            var response = await _villaService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Villa Deleted Successfully!";
                return RedirectToAction(nameof(IndexVilla));
            }
            TempData["error"] = "Error encountered.";

            return View(model);
        }

        #endregion





    }
}

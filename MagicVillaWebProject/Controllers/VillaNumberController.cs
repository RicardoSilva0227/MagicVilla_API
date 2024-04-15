using AutoMapper;
using MagicVillaWebProject.Models;
using MagicVillaWebProject.Models.Dto;
using MagicVillaWebProject.Models.ViewModels;
using MagicVillaWebProject.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVillaWebProject.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }


        #region Index
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> list = new();

            var response = await _villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.result));
            }
            return View(list);
        }
        #endregion

        #region Create
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new VillaNumberCreateVM();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.result))
                    .Select(i=> new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }

            var resp = await _villaService.GetAllAsync<APIResponse>();
            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(resp.result))
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }
        #endregion

        //#region Update
        //public async Task<IActionResult> UpdateVillaNumber(int villaId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _villaService.GetAsync<APIResponse>(villaId);
        //        if (response != null && response.IsSuccess)
        //        {
        //            VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.result));
        //            return View(_mapper.Map<VillaUpdateDTO>(model));
        //        }
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateDTO model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _villaNumberService.UpdateAsync<APIResponse>(model);
        //        if (response != null && response.IsSuccess)
        //        {
        //            return RedirectToAction(nameof(IndexNumberVilla));
        //        }
        //    }
        //    return View(model);
        //}

        //#endregion

        //#region Delete
        //public async Task<IActionResult> DeleteVillaNumber(int villaId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var response = await _villaService.GetAsync<APIResponse>(villaId);
        //        if (response != null && response.IsSuccess)
        //        {
        //            VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.result));
        //            return View(model);
        //        }
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO model)
        //{
        //    var response = await _villaService.DeleteAsync<APIResponse>(model.Id);
        //    if (response != null && response.IsSuccess)
        //    {
        //        return RedirectToAction(nameof(IndexVilla));
        //    }

        //    return View(model);
        //}

        //#endregion
    }
}

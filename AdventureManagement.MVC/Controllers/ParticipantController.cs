using AdventureManagement.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AdventureManagement.MVC.Models;
using AdventureManagement.MVC.Service.Interface;

namespace AdventureManagement.MVC.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var participants = await _participantService.GetAllParticipantsAsync();
                return View(participants);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<ParticipantVM>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var participant = await _participantService.GetParticipantByIdAsync(id);
                return View(participant);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParticipantCreateVM participant)
        {
            if (!ModelState.IsValid)
                return View(participant);

            try
            {
                var result = await _participantService.CreateParticipantAsync(participant);
                if (result)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(participant);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var participant = await _participantService.GetParticipantByIdAsync(id);
                return View(participant);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ParticipantUpdateVM participant)
        {
            if (!ModelState.IsValid)
                return View(participant);

            try
            {
                var result = await _participantService.UpdateParticipantAsync(id, participant);
                if (result)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(participant);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var participant = await _participantService.GetParticipantByIdAsync(id);
                return View(participant);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _participantService.DeleteParticipantAsync(id);
                if (result)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

using alternatrr.Models;
using alternatrr.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace alternatrr.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly SonarrDbContext _sonarrDbContext;
        private readonly SceneMappingService _sceneMappingService;

        public HomeController(SonarrDbContext sonarrDbContext, SceneMappingService sceneMappingService)
        {
            _sonarrDbContext = sonarrDbContext;
            _sceneMappingService = sceneMappingService;
        }

        public async Task<IActionResult> Index()
        {
            return View(new IndexViewModel
            {
                Series = await _sonarrDbContext.Series.ToListAsync()
            });
        }

        public async Task<IActionResult> Mappings(long id)
        {
            var series = await _sonarrDbContext.Series.FirstOrDefaultAsync(x => x.Id == id);
            if (series == null) return View("Error");

            var mappings = await _sonarrDbContext.SceneMappings.Where(x => x.TvdbId == series.TvdbId).ToListAsync();

            return View(new MappingsViewModel()
            {
                Series = series,
                SceneMappings = mappings
            });
        }


        [HttpGet]
        public async Task<IActionResult> AddMapping(long id)
        {
            var series = await _sonarrDbContext.Series.FirstOrDefaultAsync(x => x.Id == id);
            if (series == null) return View("Error");

            return View(new AddMappingInputModel()
            {
                Series = series,
                SeriesId = series.Id,
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddMapping(AddMappingInputModel model)
        {
            var series = await _sonarrDbContext.Series.FirstOrDefaultAsync(x => x.Id == model.SeriesId);
            if (series == null) return View("Error");

            await _sonarrDbContext.SceneMappings.AddAsync(new SceneMapping()
            {
                SeasonNumber = -1,
                TvdbId = series.TvdbId,
                ParseTerm = _sceneMappingService.CleanParseTitle(model.SearchTerm),
                SearchTerm = _sceneMappingService.CleanSearchTitle(model.SearchTerm),
                Title = model.SearchTerm,
                Type = "alternatrr"
            });
            await _sonarrDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Mappings), new { id = model.SeriesId });
        }


        [HttpGet]
        public async Task<IActionResult> DeleteMapping(long id)
        {
            var mapping = await _sonarrDbContext.SceneMappings.FirstOrDefaultAsync(x => x.Id == id);
            if (mapping == null) return View("Error");

            var series = await _sonarrDbContext.Series.FirstOrDefaultAsync(x => x.TvdbId == mapping.TvdbId);

            return View(new DeleteMappingViewModel()
            {
                MappingId = mapping.Id,
                Series = series,
                SceneMapping = mapping
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMapping(DeleteMappingViewModel model)
        {
            if (!ModelState.IsValid) return View("Error");

            var mapping = await _sonarrDbContext.SceneMappings.FirstOrDefaultAsync(x => x.Id == model.MappingId);
            if (mapping == null) return View("Error");

            var series = await _sonarrDbContext.Series.FirstOrDefaultAsync(x => x.TvdbId == mapping.TvdbId);
            if (series == null) return View("Error");

            _sonarrDbContext.SceneMappings.Remove(mapping);
            await _sonarrDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Mappings), new { id = series.Id });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
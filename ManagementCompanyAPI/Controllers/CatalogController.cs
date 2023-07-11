using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ManagementCompanyAPI.Interfaces;
using ManagementCompanyAPI.Models;
using ManagementCompanyAPI.Repository;

namespace ManagementCompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        private readonly CatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        public CatalogController(CatalogRepository catalogRepository, IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Catalog>))]
        public IActionResult GetCatalogs()
        {
            var catalogs = _mapper.Map<List<CatalogDto>>(_catalogRepository.GetCatalogs());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(catalogs);
        }

        [HttpGet("{catalogId}")]
        [ProducesResponseType(200, Type = typeof(Catalog))]
        [ProducesResponseType(400)]
        public IActionResult GetCatalog(int catalogId)
        {
            if (!_catalogRepository.CatalogExists(catalogId))
                return NotFound();

            var catalog = _mapper.Map<CatalogDto>(_catalogRepository.GetCatalog(catalogId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(catalog);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCatalog([FromBody] CatalogDto catalogCreate)
        {
            if (catalogCreate == null)
                return BadRequest(ModelState);
            var f = _catalogRepository.GetCatalogs();
            var catalog = _catalogRepository
                .GetCatalogs()
                .FirstOrDefault(c => c.Title.Trim().ToUpper() == catalogCreate.Title.TrimEnd().ToUpper());

            if (catalog != null)
            {
                ModelState.AddModelError("", "Catalog already exists");
                return StatusCode(422, ModelState);
            }

            var catalogMap = _mapper.Map<Catalog>(catalogCreate);

            if (!_catalogRepository.CreateCatalog(catalogMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{catalogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCatalog(int catalogId, [FromBody] CatalogDto updatedCatalog)
        {
            if (updatedCatalog == null)
                return BadRequest(ModelState);

            if (catalogId != updatedCatalog.Id)
                return BadRequest(ModelState);

            if (!_catalogRepository.CatalogExists(catalogId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var catalogMap = _mapper.Map<Catalog>(updatedCatalog);

            if (!_catalogRepository.UpdateCatalog(catalogMap))
            {
                ModelState.AddModelError("", "Something went wrong updating category");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{catalogId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCatalog(int catalogId)
        {
            if (!_catalogRepository.CatalogExists(catalogId))
            {
                return NotFound();
            }

            var catalogToDelete = _catalogRepository.GetCatalog(catalogId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_catalogRepository.DeleteCatalog(catalogToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}

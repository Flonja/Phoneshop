using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phoneshop.Domain.Entities;
using PhoneshopNuget.Repository;

namespace Phoneshop.Api.Controllers
{
    [Authorize(Policy = UserTypeNames.Colleague)]
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IRepository<Brand> _brandRepository;

        public BrandController(IRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }

        // GET: api/brand
        [HttpGet]
        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return await _brandRepository.GetAll();
        }

        // POST: api/brand/create
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("create")]
        public async Task<Brand> CreateBrand(Brand brand)
        {
            await _brandRepository.Add(brand);

            return brand;
        }

        // DELETE: api/brand?id=5
        [HttpDelete]
        public async Task<IActionResult> DeleteBrand([FromQuery] int id)
        {
            if (!await _brandRepository.Remove(brand => brand.Id == id))
                return NotFound();

            return NoContent();
        }
    }
}

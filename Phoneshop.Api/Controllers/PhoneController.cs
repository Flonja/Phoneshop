using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Phoneshop.Domain.Entities;
using Phoneshop.Api.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PhoneshopNuget.Repository;
using Microsoft.Extensions.Options;

namespace Phoneshop.Api.Controllers
{
    [Route("api/phone")]
    [ApiController]
    public class PhoneController : ErrorControllerBase
    {
        private readonly IRepository<Phone> _phoneRepository;
        private readonly IRepository<Brand> _brandRepository;
        private readonly IMapper _mapper;

        public PhoneController(IRepository<Phone> phoneRepository, IRepository<Brand> brandRepository, IMapper mapper, IOptions<ApiBehaviorOptions> apiBehaviorOptions) : base(apiBehaviorOptions)
        {
            _phoneRepository = phoneRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        // GET: api/phone
        [HttpGet]
        public async Task<IEnumerable<Phone>> GetPhones()
        {
            return await _phoneRepository.GetAll(include: query => query.Include(ph => ph.Brand));
        }

        // GET: api/phone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phone>> GetPhone(int id)
        {
            var phone = await _phoneRepository.Get(phone => phone.Id == id, query => query.Include(ph => ph.Brand));
            if (phone == null)
                return NotFound();

            return phone;
        }

        // POST: api/phone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = UserTypeNames.Colleague)]
        [HttpPost]
        public async Task<IActionResult> CreatePhone(PhoneCreateDTO model)
        {
            var brand = await _brandRepository.Get(brand => brand.Name.ToLower() == model.Brand.ToLower());
            if (brand == null)
            {
                await _brandRepository.Add(new Brand { Name = model.Brand });

                brand = await _brandRepository.Get(brand => brand.Name == model.Brand);
            }

            if (await PhoneExists(brand.Name, model.Name))
                return this.AddModelErrors("Phone already exists");

            var phone = _mapper.Map<Phone>(model);
            phone.BrandId = brand.Id;

            await _phoneRepository.Add(phone);

            return CreatedAtAction("GetPhone", new { id = phone.Id }, phone);
        }

        // PUT: api/phone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = UserTypeNames.Colleague)]
        [HttpPut]
        public async Task<IActionResult> UpdatePhone(PhoneUpdateDTO model)
        {
            var phone = await _phoneRepository.Get(phone => phone.Id == model.Id);
            if (phone == null)
                return NotFound();

            var mappedPhone = _mapper.Map(model, phone);

            await _phoneRepository.Update(mappedPhone);
            return NoContent();
        }

        // DELETE: api/phone?id=5
        [Authorize(Policy = UserTypeNames.Colleague)]
        [HttpDelete]
        public async Task<IActionResult> DeletePhone([FromQuery] int id)
        {
            if (!await _phoneRepository.Remove(phone => phone.Id == id))
                return NotFound();

            return NoContent();
        }

        private async Task<bool> PhoneExists(string brandName, string phoneName)
        {
            var brand = await _brandRepository.Get(brand => brand.Name == brandName);
            if (brand == null)
                return false;

            return (await _phoneRepository.Get(phone => brand.Id == phone.BrandId && phoneName == phone.Name)) != null;
        }
    }
}

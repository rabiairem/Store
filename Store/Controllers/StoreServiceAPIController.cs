using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreServiceAPI.Entities;
using StoreServiceAPI.Models;
using StoreServiceAPI.Repository;

namespace StoreServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreServiceAPIController : ControllerBase
    {
        private IStoreRepository _storeRepository;
        private IMapper _mapper;
        public StoreServiceAPIController(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _storeRepository.GetStoresAsync();
            var results = _mapper.Map<IList<StoreDTO>>(stores);

            return Ok(results);
        }

        [HttpGet("{id:int}/{name}", Name = "GetStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStoreByNameAndSapNumber(int id, string name)
        {
            StoreDTO storeDTO = await _storeRepository.GetStoreByNameAndSapNumberAsync(id, name);

            return Ok(storeDTO);
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStore([FromBody] StoreDTO storeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var store = _mapper.Map<Store>(storeDTO);
            await _storeRepository.CreateStoreAsync(storeDTO);

            return CreatedAtRoute("CreateStore", new { id = store.SapNumber, name = store.Name }, store);
        }

        [Authorize]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] StoreDTO storeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var store = await _storeRepository.GetStoreByNameAndSapNumberAsync(id, storeDTO.Name);
            if (store == null)
            {
                return BadRequest("Submitted data is invalid");
            }

            _mapper.Map(storeDTO, store);
            await _storeRepository.UpdateStoreAsync(store);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var store = await _storeRepository.GetStoreBySapNumberAsync(id);
            if (store == null)
            {
                return BadRequest("Submitted data is invalid");
            }

            await _storeRepository.DeleteStoreAsync(id);

            return NoContent();
        }
    }
}

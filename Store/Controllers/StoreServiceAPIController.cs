using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpGet(Name = "GetStores")]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetStoreByNameAndSapNumber(int id, string name)
        {
            StoreDTO storeDTO = await _storeRepository.GetStoreByNameAndSapNumberAsync(id, name);

            return Ok(storeDTO);
        }

        [HttpPost("CreateStore", Name = "CreateStore")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStore([FromBody] StoreDTO storeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storeDtoFromDb = await _storeRepository.CreateStoreAsync(storeDTO);

            return Ok(storeDtoFromDb);
        }

        private async Task<IEnumerable<StoreDTO>> GetStoresFromJson()
        {
            var result = new List<StoreDTO>();

            using (StreamReader r = new StreamReader("stores.json"))
            {
                string json = await r.ReadToEndAsync();
                result = JsonConvert.DeserializeObject<List<StoreDTO>>(json);
            }

            return result;
        }

        [HttpPost("CreateOrUpdateStoresFromJson", Name = "CreateOrUpdateStoresFromJson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrUpdateStoresFromJson()
        {
            var storesFromJson = await GetStoresFromJson();

            var storesDtoFromDb = await _storeRepository.CreateStoresAsync(storesFromJson);

            return Ok(storesDtoFromDb);
        }

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

            var storeDb = await _storeRepository.UpdateStoreAsync(storeDTO);

            return Ok(storeDb);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var store = await _storeRepository.GetStoreBySapNumberAsync(id);
            if (store == null)
            {
                return BadRequest("Submitted data is invalid");
            }

            var result = await _storeRepository.DeleteStoreAsync(id);

            return Ok(result);
        }
    }
}

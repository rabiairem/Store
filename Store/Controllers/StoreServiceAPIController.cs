using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreServiceAPI.DataAccess.Services;
using StoreServiceAPI.Models;

namespace StoreServiceAPI.Controllers
{
    [Route("api/storeservice")]
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
            var store = await _storeRepository.GetStoreByNameAndSapNumberAsync(id, name);

            return Ok(_mapper.Map<StoreDTO>(store));
        }

        [HttpPost("CreateStore", Name = "CreateStore")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStore([FromBody] StoreDTO storeDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storeFromDb = await _storeRepository.CreateStoreAsync(storeDTO);

            return Ok(_mapper.Map(storeFromDb, storeDTO));
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

        [HttpPost("CreateStoresFromJson", Name = "CreateStoresFromJson")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStoresFromJson()
        {
            var storesFromJson = await GetStoresFromJson();

            var storesFromDb = await _storeRepository.CreateStoresAsync(storesFromJson);

            if (storesFromDb == null || !storesFromDb.Any())
                return BadRequest("There is nothing to add");

            return Ok(_mapper.Map<IEnumerable<StoreDTO>>(storesFromDb));
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

            return Ok(_mapper.Map(storeDb, storeDTO));
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

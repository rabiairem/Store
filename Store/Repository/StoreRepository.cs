using StoreServiceAPI.Models;

namespace StoreServiceAPI.Repository
{
    public class StoreRepository : IStoreRepository
    {
        public Task<StoreDTO> CreateStore(StoreDTO storeDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteStore(int storeId)
        {
            throw new NotImplementedException();
        }

        public Task<StoreDTO> GetStoreById(int storeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StoreDTO>> GetStores()
        {
            throw new NotImplementedException();
        }

        public Task<StoreDTO> UpdateStore(StoreDTO storeDto)
        {
            throw new NotImplementedException();
        }
    }
}

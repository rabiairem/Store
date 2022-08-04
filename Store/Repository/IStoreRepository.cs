using StoreServiceAPI.Models;

namespace StoreServiceAPI.Repository
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreDTO>> GetStores();
        Task<StoreDTO> GetStoreById(int storeId);
        Task<StoreDTO> CreateStore(StoreDTO storeDto);
        Task<StoreDTO> UpdateStore(StoreDTO storeDto);
        Task<bool> DeleteStore(int storeId);
    }
}

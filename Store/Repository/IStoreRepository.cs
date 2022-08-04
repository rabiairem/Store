using StoreServiceAPI.Models;

namespace StoreServiceAPI.Repository
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreDTO>> GetStoresAsync();
        Task<StoreDTO> GetStoreByNameAndSapNumberAsync(int sapNumber, string name);
        Task<StoreDTO> GetStoreBySapNumberAsync(int sapNumber);
        Task<StoreDTO> CreateStoreAsync(StoreDTO storeDto);
        Task<StoreDTO> UpdateStoreAsync(StoreDTO storeDto);
        Task<bool> DeleteStoreAsync(int storeId);
    }
}

using StoreServiceAPI.DataAccess.Entities;
using StoreServiceAPI.Models;

namespace StoreServiceAPI.DataAccess.Services
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetStoresAsync();
        Task<Store> GetStoreBySapNumberAndNameAsync(int sapNumber, string name);
        Task<Store> GetStoreBySapNumberAsync(int sapNumber);
        Task<Store> CreateStoreAsync(StoreDTO storeDto);
        Task<IEnumerable<Store>> CreateStoresAsync(IEnumerable<StoreDTO> results);
        Task<Store> UpdateStoreAsync(StoreDTO storeDto);
        Task<bool> DeleteStoreAsync(int storeId);
    }
}

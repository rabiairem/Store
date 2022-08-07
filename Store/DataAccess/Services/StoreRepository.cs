using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreServiceAPI.DataAccess.DbContexts;
using StoreServiceAPI.DataAccess.Entities;
using StoreServiceAPI.Models;

namespace StoreServiceAPI.DataAccess.Services
{
    public class StoreRepository : IStoreRepository
    {
        private readonly DataBaseContext _db;
        private IMapper _mapper;

        public StoreRepository(DataBaseContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Store> CreateStoreAsync(StoreDTO storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);

            if (await GetStoreBySapNumberAsync(store.SapNumber_id) == null)
                await _db.Stores.AddAsync(store);

            await _db.SaveChangesAsync();
            return store;
        }

        public async Task<IEnumerable<Store>> CreateStoresAsync(IEnumerable<StoreDTO> storeDTOs)
        {
            var stores = _mapper.Map<IEnumerable<Store>>(storeDTOs);

            var dataForAdd = stores.Where(s => !_db.Stores.AsQueryable().AsNoTracking().Contains(s)).ToList();

            if (dataForAdd != null)
                await _db.Stores.AddRangeAsync(dataForAdd);

            await _db.SaveChangesAsync();

            return dataForAdd;
        }

        public async Task<bool> DeleteStoreAsync(int storeSapNumber)
        {
            try
            {
                Store store = await _db.Stores.Where(s => s.SapNumber_id == storeSapNumber).AsNoTracking().FirstOrDefaultAsync();

                if (store == null)
                    return false;

                _db.Stores.Remove(store);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Store> GetStoreBySapNumberAndNameAsync(int storeSapNumber, string name)
        {
            Store store = await _db.Stores.Where(x => x.SapNumber_id ==
            storeSapNumber && x.Name == name).AsNoTracking().FirstOrDefaultAsync();

            return store;
        }

        public async Task<Store> GetStoreBySapNumberAsync(int sapNumber)
        {
            Store store = await _db.Stores.Where(x => x.SapNumber_id == sapNumber).AsNoTracking().FirstOrDefaultAsync();
            return store;
        }

        public async Task<IEnumerable<Store>> GetStoresAsync()
        {
            return await _db.Stores.AsNoTracking().ToListAsync();
        }

        public async Task<Store> UpdateStoreAsync(StoreDTO storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);

            if (await GetStoreBySapNumberAsync(store.SapNumber_id) != null)
            {
                _db.Update(store);
                await _db.SaveChangesAsync();
            }

            return store;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StoreServiceAPI.DbContexts;
using StoreServiceAPI.Entities;
using StoreServiceAPI.Models;

namespace StoreServiceAPI.Repository
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

        public async Task<StoreDTO> CreateStoreAsync(StoreDTO storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);

            if (await GetStoreBySapNumberAsync(store.SapNumber_id) == null)
                await _db.Stores.AddAsync(store);
            else
                _db.Stores.Update(store);

            await _db.SaveChangesAsync();
            return _mapper.Map(store, storeDto);
        }

        public async Task<IEnumerable<StoreDTO>> CreateStoresAsync(IEnumerable<StoreDTO> storeDTOs)
        {
            var stores = _mapper.Map<IEnumerable<Store>>(storeDTOs);

            var dataForUpdate = stores.Where(s => _db.Stores.AsQueryable().AsNoTracking().Contains(s));
            var dataForAdd = stores.Where(s => !_db.Stores.AsQueryable().AsNoTracking().Contains(s));

            if (dataForUpdate != null)
                _db.Stores.UpdateRange(dataForUpdate);

            if (dataForAdd != null)
                await _db.Stores.AddRangeAsync(dataForAdd);

            await _db.SaveChangesAsync();

            return _mapper.Map(stores, storeDTOs);
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

        public async Task<StoreDTO> GetStoreByNameAndSapNumberAsync(int storeSapNumber, string name)
        {
            Store store = await _db.Stores.Where(x => x.SapNumber_id ==
            storeSapNumber && x.Name == name).AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<StoreDTO>(store);
        }

        public async Task<StoreDTO> GetStoreBySapNumberAsync(int sapNumber)
        {
            Store store = await _db.Stores.Where(x => x.SapNumber_id == sapNumber).AsNoTracking().FirstOrDefaultAsync();
            return _mapper.Map<StoreDTO>(store);
        }

        public async Task<IEnumerable<StoreDTO>> GetStoresAsync()
        {
            List<Store> storeList = await _db.Stores.AsNoTracking().ToListAsync();
            return _mapper.Map<List<StoreDTO>>(storeList);
        }

        public async Task<StoreDTO> UpdateStoreAsync(StoreDTO storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);

            if (await GetStoreBySapNumberAsync(store.SapNumber_id) != null)
            {
                _db.Update(store);
                await _db.SaveChangesAsync();
            }

            return _mapper.Map(store, storeDto);
        }
    }
}

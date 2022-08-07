using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoreServiceAPI.Configurations;
using StoreServiceAPI.Controllers;
using StoreServiceAPI.DataAccess.Entities;
using StoreServiceAPI.DataAccess.Services;
using StoreServiceAPI.Models;

namespace StoreServiceAPI.Test
{
    public class StoreServiceAPITests
    {
        private StoreServiceAPIController _storeServiceAPIController;
        private Mock<IStoreRepository> _storeRepositoryMock;
        private Mapper _mapper;

        public StoreServiceAPITests()
        {
            var mapperConfiguration = new MapperConfiguration(
               cfg => cfg.AddProfile<MapperInitializer>());
            _mapper = new Mapper(mapperConfiguration);

            _storeRepositoryMock = new Mock<IStoreRepository>();
            var storeList = new List<Store>{new Store
            {
                Name = "JFM Keuken Tilburg",
                SapNumber_id = 4771,
                Abbreviation = "TFM",
                SmsStoreNumber = 866,
                IsFranchise = false,
                PostalCode = "5022 DD",
                Province = "Noord Brabant",
                IsOpenOnSunday = true
            },
            new Store
            {
                Name = "JFM Keuken Utrecht",
                SapNumber_id = 4784,
                Abbreviation = "UFM",
                SmsStoreNumber = 841,
                IsFranchise = false,
                PostalCode = "3541 CH",
                Province = "Utrecht",
                IsOpenOnSunday = true
            },
            new Store
            {
                  Name = "JFM Keuken Veghel",
                  SapNumber_id = 4725,
                  Abbreviation = "VFM",
                  SmsStoreNumber = 280,
                  IsFranchise = false,
                  PostalCode = "5462 EH",
                  Province = "Noord Brabant",
                  IsOpenOnSunday = true
            }};
            _storeRepositoryMock.Setup(sr => sr.GetStoresAsync()).ReturnsAsync(storeList);

            _storeRepositoryMock.Setup(sr =>
            sr.CreateStoreAsync(It.IsAny<StoreDTO>())).ReturnsAsync(new Store
            {
                Name = "Jumbo Aalsmeer Ophelialaan",
                SapNumber_id = 3178,
                Abbreviation = "AOP",
                SmsStoreNumber = 824,
                IsFranchise = false,
                PostalCode = "1431 HN",
                Province = "Noord Holland",
                IsOpenOnSunday = true,
                FlowersModule = 1
            });

            _storeRepositoryMock.Setup(sr =>
          sr.UpdateStoreAsync(It.IsAny<StoreDTO>())).ReturnsAsync(new Store
          {
              Name = "JFM Aalsmeer Ophelialaan",
              SapNumber_id = 4771,
              Abbreviation = "TFM",
              SmsStoreNumber = 866,
              IsFranchise = true,
              PostalCode = "5022 DD",
              Province = "Noord Holland",
              IsOpenOnSunday = false
          });
        }

        [Fact]
        public async Task GetStores_GetAction_MustReturnOkObjectResult()
        {
            // Arrange
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            // Act
            var result = await _storeServiceAPIController.GetStores();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetStores_GetAction_MatchResult()
        {
            //Arrange  
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var result = await _storeServiceAPIController.GetStores();

            //Assert  
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var storeDTOs = Assert.IsAssignableFrom<IEnumerable<StoreDTO>>(okObjectResult.Value);
            var firstDTO = storeDTOs.First();

            Assert.Equal("JFM Keuken Tilburg", firstDTO.Name);
            Assert.Equal(4771, firstDTO.SapNumber);
            Assert.Equal("TFM", firstDTO.Abbreviation);
            Assert.Equal(866, firstDTO.SmsStoreNumber);
            Assert.False(firstDTO.IsFranchise);
            Assert.Equal("5022 DD", firstDTO.PostalCode);
            Assert.Equal("Noord Brabant", firstDTO.Province);
            Assert.True(firstDTO.IsOpenOnSunday);
        }

        [Theory]
        [InlineData("JFM Keuken Tilburg", 4771)]
        public async void GetStoreBySapNumberAndName_Return_OkResult(string name, int sapNumber)
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
            sr.GetStoreBySapNumberAndNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Store
            {
                Name = "JFM Keuken Tilburg",
                SapNumber_id = 4771,
                Abbreviation = "TFM",
                SmsStoreNumber = 866,
                IsFranchise = false,
                PostalCode = "5022 DD",
                Province = "Noord Brabant",
                IsOpenOnSunday = true
            });
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var result = await _storeServiceAPIController.GetStoreBySapNumberAndName(sapNumber, name);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("Jumbo Aalsmeer Ophelialaan", 3178)]
        public async void GetStoreBySapNumberAndName_Return_NotFoundResult(string name, int sapNumber)
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAndNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((Store)null);
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var result = await _storeServiceAPIController.GetStoreBySapNumberAndName(sapNumber, name);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("JFM Keuken Tilburg", 4771)]
        public async void GetStoreBySapNumberAndName_MatchResult(string name, int sapNumber)
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAndNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Store
           {
               Name = "JFM Keuken Tilburg",
               SapNumber_id = 4771,
               Abbreviation = "TFM",
               SmsStoreNumber = 866,
               IsFranchise = false,
               PostalCode = "5022 DD",
               Province = "Noord Brabant",
               IsOpenOnSunday = true
           });
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var result = await _storeServiceAPIController.GetStoreBySapNumberAndName(sapNumber, name);

            //Assert  
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var storeDTO = Assert.IsAssignableFrom<StoreDTO>(okObjectResult.Value);

            Assert.Equal(name, storeDTO.Name);
            Assert.Equal(sapNumber, storeDTO.SapNumber);
            Assert.Equal("TFM", storeDTO.Abbreviation);
            Assert.Equal(866, storeDTO.SmsStoreNumber);
            Assert.False(storeDTO.IsFranchise);
            Assert.Equal("5022 DD", storeDTO.PostalCode);
            Assert.Equal("Noord Brabant", storeDTO.Province);
            Assert.True(storeDTO.IsOpenOnSunday);
        }


        [Fact]
        public async void CreateStore_Return_OkResult()
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAndNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Store
           {
               Name = "JFM Keuken Tilburg",
               SapNumber_id = 4771,
               Abbreviation = "TFM",
               SmsStoreNumber = 866,
               IsFranchise = false,
               PostalCode = "5022 DD",
               Province = "Noord Brabant",
               IsOpenOnSunday = true
           });

            var storeDTO = new StoreDTO
            {
                Name = "Jumbo Aalsmeer Ophelialaan",
                SapNumber = 3178,
                Abbreviation = "AOP",
                SmsStoreNumber = 824,
                IsFranchise = false,
                PostalCode = "1431 HN",
                Province = "Noord Holland",
                IsOpenOnSunday = true,
                FlowersModule = 1
            };
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var result = await _storeServiceAPIController.CreateStore(storeDTO);

            //Assert  
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async void CreateStore_MatchResult()
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAndNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Store
           {
               Name = "JFM Keuken Tilburg",
               SapNumber_id = 4771,
               Abbreviation = "TFM",
               SmsStoreNumber = 866,
               IsFranchise = false,
               PostalCode = "5022 DD",
               Province = "Noord Brabant",
               IsOpenOnSunday = true
           });

            var storeDTO = new StoreDTO
            {
                Name = "Jumbo Aalsmeer Ophelialaan",
                SapNumber = 3178,
                Abbreviation = "AOP",
                SmsStoreNumber = 824,
                IsFranchise = false,
                PostalCode = "1431 HN",
                Province = "Noord Holland",
                IsOpenOnSunday = true,
                FlowersModule = 1
            };
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var result = await _storeServiceAPIController.CreateStore(storeDTO);

            //Assert  
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var resultStoreDTO = Assert.IsAssignableFrom<StoreDTO>(createdAtRouteResult.Value);

            Assert.Equal(storeDTO.Name, resultStoreDTO.Name);
            Assert.Equal(storeDTO.SapNumber, resultStoreDTO.SapNumber);
            Assert.Equal(storeDTO.Abbreviation, resultStoreDTO.Abbreviation);
            Assert.Equal(storeDTO.SmsStoreNumber, resultStoreDTO.SmsStoreNumber);
            Assert.False(resultStoreDTO.IsFranchise);
            Assert.Equal(storeDTO.PostalCode, resultStoreDTO.PostalCode);
            Assert.Equal(storeDTO.Province, resultStoreDTO.Province);
            Assert.True(resultStoreDTO.IsOpenOnSunday);
        }

        [Fact]
        public async void UpdateStore_Return_OkResult()
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAndNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Store
           {
               Name = "JFM Keuken Tilburg",
               SapNumber_id = 4771,
               Abbreviation = "TFM",
               SmsStoreNumber = 866,
               IsFranchise = false,
               PostalCode = "5022 DD",
               Province = "Noord Brabant",
               IsOpenOnSunday = true
           });

            var storeDTO = new StoreDTO
            {
                Name = "JFM Aalsmeer Ophelialaan",
                SapNumber = 4771,
                Abbreviation = "TFM",
                SmsStoreNumber = 866,
                IsFranchise = true,
                PostalCode = "5022 DD",
                Province = "Noord Holland",
                IsOpenOnSunday = false
            };
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var result = await _storeServiceAPIController.UpdateStore(storeDTO.SapNumber, storeDTO);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);

            //Assert  
            Assert.IsType<OkObjectResult>(okObjectResult);
        }

        [Fact]
        public async void UpdateStore_NonExistingData_Return_BadRequest()
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAndNameAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((Store)null);

            var storeDTO = new StoreDTO
            {
                Name = "JFM Aalsmeer Ophelialaan",
                SapNumber = 1,
                Abbreviation = "TFM",
                SmsStoreNumber = 866,
                IsFranchise = true,
                Province = "Noord Holland",
                IsOpenOnSunday = false
            };
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var updatedData = await _storeServiceAPIController.UpdateStore(storeDTO.SapNumber, storeDTO);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(updatedData);
        }

        [Fact]
        public async void DeleteStore_OkResult()
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAsync(It.IsAny<int>())).ReturnsAsync(new Store
           {
               Name = "JFM Keuken Tilburg",
               SapNumber_id = 4771,
               Abbreviation = "TFM",
               SmsStoreNumber = 866,
               IsFranchise = false,
               PostalCode = "5022 DD",
               Province = "Noord Brabant",
               IsOpenOnSunday = true
           });
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var data = await _storeServiceAPIController.DeleteStore(1028);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange  
            _storeRepositoryMock.Setup(sr =>
           sr.GetStoreBySapNumberAsync(It.IsAny<int>())).ReturnsAsync((Store)null);
            _storeServiceAPIController = new StoreServiceAPIController(_storeRepositoryMock.Object, _mapper);

            //Act  
            var data = await _storeServiceAPIController.DeleteStore(1);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data);
        }
    }
}

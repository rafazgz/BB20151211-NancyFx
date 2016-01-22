using System.Collections.Generic;
using App.Models;
using App.Modules;
using Common.ViewModels;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using Test.Mocks;
using Xunit;

namespace Test.ModuleTests
{
    public class RoomModuleTest
    {
        private readonly Browser _browser;
        public RoomModuleTest()
        {
            Common.Setup.AutoMapper.Initialize();
            _browser = new Browser(with => with.Module(new RoomModule(new RoomServiceMock())));
        }

        [Fact]
        public void listing_all_chat_rooms_returns_a_list_of_RoomViewModel()
        {
            var response = _browser.Get("/api/rooms/list", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.Header("Content-Type", "application/json");
            });

            var content = response.Body.DeserializeJson<IList<RoomViewModel>>();

            Assert.Contains(content, room => room.Name == "DotNetters");
        }

        [Fact]
        public void should_use_NewRoomModel_when_creating_a_new_chat_room()
        {
            var newRoom = new NewRoomModel{Name = "BetaBeers"};

            var response = _browser.Get("/api/rooms/list", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(newRoom));
            });

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void creating_a_new_chat_room_should_return_a_RoomViewModel()
        {
            var newRoom = new NewRoomModel { Name = "BetaBeers" };

            var response = _browser.Post("/api/rooms/new", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.Header("Content-Type", "application/json");
                with.Body(JsonConvert.SerializeObject(newRoom));
            });

            var content = response.Body.DeserializeJson<RoomViewModel>();

            Assert.Equal("BetaBeers", content.Name);
        }
    }
}

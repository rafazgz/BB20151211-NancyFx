(function (angular) {
    'use strict';

    var app = angular.module('app', []);

    app.controller('GlobalController', ['$scope', 'httpService', function ($scope, httpService) {
        $scope.logged = false;
        $scope.rooms = [];
        $scope.username = '';
        $scope.loggedUser = '';

        $scope.login = function(username) {
            $scope.loggedUser = username;

            httpService.getAllRooms().success(function(data) {
                console.log(data);

                $scope.rooms = data;

                $scope.logged = true;
            });
        };

        $scope.createRoom = function (roomname) {
            httpService.createRoom(roomname).success(function(data) {
                $scope.rooms.push(data);
            });
        };
    }]);

    app.service('httpService', ['$http', function ($http) {
        function getAllRooms() {
            return $http({
                method: 'GET',
                url: '/api/rooms/list'
            });
        }

        function createRoom(roomname) {
            return $http({
                method: 'POST',
                url: '/api/rooms/new',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: { name: roomname }
            });
        }

        return {
            getAllRooms: getAllRooms,
            createRoom: createRoom
        };
    }]);
}(angular));
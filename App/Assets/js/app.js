(function (angular, $) {
    'use strict';

    var app = angular.module('app', []);

    app.value('$', $);

    app.controller('GlobalController', function ($, $scope, httpService, SignalRFactory) {
        $scope.rooms = [];
        $scope.users = [];
        $scope.messages = [];
        $scope.messages.push({ user: 'Test', message: 'Lorem ...' });
        var signalR = new SignalRFactory({
            addUser: function (user) {
                $scope.$applyAsync(function() {
                    $scope.users.push(user);
                });
            },

            addMessage: function (user, message) {
                $scope.$applyAsync(function () {
                    $scope.messages.push({ user: user, message: message });
                });
            }
        });

        $scope.login = function (username) {
            $scope.loggedUser = username;

            httpService.getAllRooms().success(function (data) {
                $scope.rooms = data;

                $scope.hub = signalR.getConnection();

                $scope.logged = true;
            });
        };

        $scope.createRoom = function (roomname) {
            httpService.createRoom(roomname).success(function (data) {
                $scope.rooms.push(data);
            });
        };

        $scope.joinRoom = function (roomname) {
            $scope.hub.invoke('join', $scope.loggedUser, roomname);
            $scope.roomname = roomname;
            $scope.users.push($scope.loggedUser);
        }

        $scope.sendMessage = function (message) {
            console.log(message);
            $scope.hub.invoke('send', message);
        }
    });

    app.service('httpService', function ($http) {
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
    });

    app.factory('SignalRFactory', function ($) {

        function SignalRFactory(events) {
            this.events = events;
        }

        SignalRFactory.prototype.getConnection = function () {
            var _this = this;
            var connection = $.hubConnection();
            var hub = connection.createHubProxy('chatRoom');

            hub.on('hasJoined', function (username) {
                _this.events.addUser(username);
            });

            hub.on('addMessage', function (user, message) {
                _this.events.addMessage(user, message);
            });

            connection.start();
                
            return hub;
        }

        return SignalRFactory;
    });

    app.filter('reverse', function() {
        return function(items) {
            return items.slice().reverse();
        }
    });
}(angular, $));
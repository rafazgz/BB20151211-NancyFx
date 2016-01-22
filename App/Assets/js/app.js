(function (angular, $) {
    'use strict';

    var app = angular.module('app', []);

    app.value('$', $);

    app.controller('GlobalController', function ($, $scope, httpService, SignalRFactory) {
        $scope.rooms = [];
        $scope.users = [];
        $scope.messages = [];

        var signalR = new SignalRFactory({
            addUser: function (user) {
                $scope.$applyAsync(function () {
                    $scope.users.push(user);
                });
            },

            addMessage: function (user, message) {
                $scope.$applyAsync(function () {
                    $scope.messages.push({ user: user, message: message });
                });
            },

            disconnectUser: function(user) {
                $scope.$applyAsync(function() {
                    var index = $scope.users.indexOf(user);
                    if (index > -1) {
                        $scope.users.splice(index, 1);
                    }
                });
            }
        });

        $scope.login = function (username) {
            $scope.loggedUser = username;

            httpService.login(username);

            httpService.getAllRooms().then(function (data) {
                $scope.rooms = data.data;

                $scope.hub = signalR.getConnection();

                $scope.logged = true;
            });
        };

        $scope.createRoom = function (roomname) {
            httpService.createRoom(roomname).then(function (data) {
                $scope.rooms.push(data.data);
            });
        };

        $scope.joinRoom = function (roomname) {
            if ($scope.chatting) {
                $scope.hub.invoke('disconnect');
                setTimeout(function() {}, 1000);
            }
            httpService.getUsersInARoom(roomname).then(function (data) {
                $scope.users = data.data;

                $scope.messages = [];

                $scope.hub.invoke('join', $scope.loggedUser, roomname);

                $scope.roomname = roomname;

                $scope.users.push($scope.loggedUser);

                httpService.joinRoom($scope.loggedUser, roomname);

                $scope.chatting = true;
            });

        }

        $scope.sendMessage = function (message) {
            $scope.hub.invoke('send', message);
        }
    });

    app.service('httpService', function ($http, $q) {
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

        function getUsersInARoom(roomname) {
            var deferred = $q.defer();

            $http({
                method: 'GET',
                url: '/api/rooms/' + roomname + '/listUsers',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(function (data) {
                deferred.resolve(data);
            }, function (data) {
                deferred.reject(data);
            });

            return deferred.promise;
        }

        function login(username) {
            return $http({
                method: 'POST',
                url: '/api/users/login',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: { name: username }
            });
        }

        function joinRoom(username, roomname) {
            return $http({
                method: 'POST',
                url: '/api/rooms/' + roomname + '/join',
                headers: {
                    'Content-Type': 'application/json'
                },
                data: { user: username }
            });
        }

        return {
            getAllRooms: getAllRooms,
            createRoom: createRoom,
            login: login,
            joinRoom: joinRoom,
            getUsersInARoom: getUsersInARoom
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

            hub.on('disconnectUser', function (user) {
                _this.events.disconnectUser(user);
            });

            connection.start();

            return hub;
        }

        return SignalRFactory;
    });

    app.filter('reverse', function () {
        return function (items) {
            return items.slice().reverse();
        }
    });
}(angular, $));
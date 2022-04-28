'use strict';

angular.module('myApp.factories', [])

    .factory('customerFactory', function ($http) {
        var fcty = this;

        fcty.list = function (result) {
            return $http({
                method: 'GET',
                url: 'https://localhost:7031/api/Customer',
                params: { 'command': 2 },
            }).then(result);
        }

        fcty.obtain = function (id, result) {
            return $http({
                method: 'GET',
                url: 'https://localhost:7031/api/Customer/' + id
            }).then(result);
        }

        fcty.create = function (item, result) {
            return $http({
                method: 'POST',
                url: 'https://localhost:7031/api/Customer',
                data: {
                    'command': 4,
                    'entity': item,
                },
            }).then(result);
        }

        fcty.update = function (item, result) {
            return $http({
                method: 'PUT',
                url: 'https://localhost:7031/api/Customer/' + item.id,
                data: {
                    'command': 5,
                    'entity': item,
                }
            }).then(result);
        }

        fcty.delete = function (item, result) {
            return $http({
                method: 'PUT',
                url: 'https://localhost:7031/api/Customer/' + item.id,
                data: {
                    'command': 6,
                    'entity': item,
                }
            }).then(result);
        }

        return fcty;
    });
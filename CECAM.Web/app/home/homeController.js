'use strict';

angular.module('myApp.home', ['ngRoute'])

  .config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/home', {
      templateUrl: 'home/home.html',
      controller: 'HomeCtrl'
    });
  }])

  .controller('HomeCtrl', ['customerFactory', '$location', '$route', function (customerFactory, $location, $route) {
    var ctrl = this;

    ctrl.customers = [];

    ctrl.commands = {
      edit: editCustomer,
      delete: deleteCustomer,
    };

    startPage();

    function startPage() {
      customerFactory.list(function (result) {
        ctrl.customers = result.data.result;
      });
    }

    function editCustomer(id) {
      $location.path('/customer/' + id);
    }

    function deleteCustomer(customer) {
      customerFactory.delete(customer, function (_) {
        ctrl.customers = ctrl.customers.filter(function (el) {
          return el.id != customer.id;
        });
      });
    }
  }]);
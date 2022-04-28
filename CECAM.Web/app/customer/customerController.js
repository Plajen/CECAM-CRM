'use strict';

angular.module('myApp.customer', ['ngRoute'])

  .config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/customer/:id', {
      templateUrl: 'customer/customer.html',
      controller: 'CustomerCtrl'
    });
  }])

  .controller('CustomerCtrl', ['customerFactory', '$routeParams', '$location', function (customerFactory, $routeParams, $location) {
    var ctrl = this;

    ctrl.action = 'Editar';

    ctrl.customer = {
      id: 0,
      companyName: '',
      cnpj: '',
      createdAt: new Date(),
    };

    ctrl.commands = {
      save: save,
      create: createCustomer,
      edit: editCustomer,
    };

    startPage();

    function startPage() {
      if ($routeParams.id == 0) {
        ctrl.action = 'Cadastrar Novo';

        customerFactory.list(function (result) {
          const ids = result.data.result.map(x => {
            return x.id;
          });
          ctrl.customer.id = Math.max(...ids) + 1;
        });

        return;
      }

      customerFactory.obtain($routeParams.id, function (result) {
        ctrl.customer = result.data.result;
      });
    }

    function save() {
      if (ctrl.action == 'Editar') {
        editCustomer();
      } else {
        createCustomer();
      }
    }

    function createCustomer() {
      customerFactory.create(ctrl.customer, function (result) {
        $location.path('/customer/' + result.data.result.id);
      });
    }

    function editCustomer() {
      customerFactory.update(ctrl.customer, function (result) {
        $location.path('/customer/' + result.data.result.id);
      });
    }
  }]);
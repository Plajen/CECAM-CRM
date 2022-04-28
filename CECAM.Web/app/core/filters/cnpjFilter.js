'use strict';

angular.module('myApp.filters.cnpj-filter', [])

.filter('toCNPJ', [function() {
  return function(text) {
    return String(text).replace(/^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, "$1.$2.$3/$4-$5");
  };
}]);

'use strict';

describe('myApp.customer module', function() {

  beforeEach(module('myApp.customer'));

  describe('customer controller', function(){

    it('should ....', inject(function($controller) {
      //spec body
      var customerCtrl = $controller('CustomerCtrl');
      expect(customerCtrl).toBeDefined();
    }));

  });
});
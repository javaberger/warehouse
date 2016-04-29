var app = angular.module('ngmodule', []).controller('ngcontroller', function ($scope, ProductService) {

    getProducts();

    function getProducts() {

        ProductService.getProducts().success(function (data) {
                $scope.products = data;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
            });
    }
});

app.factory('ProductService', ['$http', function ($http) {

            var ProductService = {};

            ProductService.getProducts = function () {

                return $http.get('/Products/');

            };

    return ProductService;
}]);
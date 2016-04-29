var app = angular.module("ngmodule", []);

app.controller('ngcontroller', function ($scope, $http) {

    $http.get('/Products/').success(function (data, status, headers, config) {
        $scope.Products = data;
    }).error(function (data, status, headers, config) {
        alert("error");
    });
});
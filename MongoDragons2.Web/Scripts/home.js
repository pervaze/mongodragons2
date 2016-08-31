var app = angular.module('homeApplication', []);

app.controller('homeController', function ($scope, $http) {

    $http.get('api/dragon').success(function (data) {
        $scope.dragons = data;
    });

    $scope.spawn = function () {
        $http.post('api/dragon/spawn').success(function (data) {
            $scope.dragons.push(data);
        });
    }

    $scope.remove = function (dragon) {
        if (confirm('Are you sure?')) {
            $http.post('api/dragon/remove', JSON.stringify(dragon)).success(function (result) {
                if (result) {
                    $scope.dragons.splice($scope.dragons.indexOf(dragon), 1);
                }
            });
        }
    }
});
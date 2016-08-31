var app = angular.module('homeApplication', []);

app.controller('homeController', function ($scope, $http) {

    $http.get('api/dragon').success(function (result) {
        $scope.dragons = result.data;
    });

    $scope.spawn = function () {
        $http.post('api/dragon/spawn').success(function (data) {
            $scope.dragons.push(data);
        });
    }

    $scope.remove = function (dragon) {
        $http.post('api/dragon/remove', JSON.stringify(dragon)).success(function (result) {
            if (result.status) {
                $scope.dragons.splice($scope.dragons.indexOf(dragon), 1);
            } else {
                console.log("Error Occured");
            }
        });
    }
});
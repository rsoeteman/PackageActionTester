angular.module("umbraco")
    .controller("PackageActionTester.PackageActionTesterController", function ($scope, $http) {

        $scope.bindData = function () {
            $http.get('/umbraco/backoffice/PackageActionTester/PackageActionTesterAPI/GetALL').then(function (res) {
                $scope.packageActions = res.data;
            });
        };

        //Initialize
        $scope.bindData();
    });
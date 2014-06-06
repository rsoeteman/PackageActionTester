angular.module("umbraco")
    .controller("PackageActionTester.PackageActionTesterController", function ($scope, $http) {

        $scope.bindData = function () {
            $scope.packageActionsXML = '';
            $http.get('/umbraco/backoffice/PackageActionTester/PackageActionTesterAPI/GetALL').then(function (res) {
                $scope.packageActions = res.data;
            });
        };

        $scope.selectAction = function () {
            var action = $scope.selectedPackageAction;
            if (action != '') {
                $scope.packageActionsXML += action.SampleXMl + '\r\n';
            }
        };

        $scope.install = function () {
            var data = { Xml: 'pietjepukisgoed' };

            $http.post('/umbraco/backoffice/PackageActionTester/PackageActionTesterAPI/Install', data).then(function (res) {
                $scope.packageActionsXML = res.data;
            });
        };
        
        $scope.uninstall = function () {
            alert('uninstall');
        };
       
        //Initialize
        $scope.bindData();
    });
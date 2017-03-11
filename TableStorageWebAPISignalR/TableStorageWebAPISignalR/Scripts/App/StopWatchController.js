app.controller('stopwatchcontroller', function ($scope, stopwatchservice) {
    $scope.stopwatch = [];
    $scope.Message = "";
    $scope.userName = sessionStorage.getItem('userName');
    //loadStopwatch();

    $scope.loadStopwatch = function () {
        var promise = stopwatchservice.get($scope.UserName);
        promise.then(function (resp) {
            $scope.stopwatch = resp.data;
            $scope.Message = "Call Completed Successfully";
        }, function (err) {
            $scope.Message = "Error!!! " + err.status
        });
    };

    $scope.StartStopStopwatch = function () {
        var promise = stopwatchservice.post($scope.UserName);
        promise.then(function (resp) {
            $scope.stopwatch = resp.data;
            $scope.Message = "Call Completed Successfully";
        }, function (err) {
            $scope.Message = "Error!!! " + err.status
        });
    };

    $scope.logout = function () {
        sessionStorage.removeItem('accessToken');
        window.location.href = '/Login/SecurityInfo';
    };

});
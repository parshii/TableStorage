app.service('stopwatchservice', function ($http) {

    this.get = function (UserName) {

        var accesstoken = sessionStorage.getItem('accessToken');

        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }

        var response = $http({
            url: "/api/Values",
            method: "GET",
            headers: authHeaders,
            params: { UserName: UserName }

        });
        return response;
    };

    this.post = function (model) {
        var accesstoken = sessionStorage.getItem('accessToken');

        var authHeaders = {};
        if (accesstoken) {
            authHeaders.Authorization = 'Bearer ' + accesstoken;
        }

        var response = $http({
            url: "/api/Values",
            method: "POST",
            headers: authHeaders,
            params: { UserName: model }
        });
        return response;
    };

});
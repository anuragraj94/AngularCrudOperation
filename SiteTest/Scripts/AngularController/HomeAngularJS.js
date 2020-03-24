var app = angular.module("Create", []);

app.controller("Controller", function ($scope, $http) {
    $scope.btnSave = "Save";

    // save data in database
    $scope.Save = function () {
        $scope.btnSave = "Please Wait....";
        $http({
            method: 'POST',
            url: '/Home/Create',
            data: $scope.UserModel
        }).then(function (d) {            
            $scope.btnSave = "Save";
            $scope.UserModel = null;
            alert(d.data);
        }, function () {
            alert('Failed');
        });        
    };

    // show data in table
    $http.get("/Home/Get_data").then(function (d) {
        $scope.record = d.data;
    }, function (error) {
        alert('Failed');
    });

    // delete record by id 
    $scope.deleterecord = function (id) {
        $http({
            method: 'POST',
            url: '/Home/Delete',
            data: { ID: id }
        }).then(function (d) {
            $scope.btnSave = "Save";
            $scope.UserModel = null;            
            $http.get("/Home/Get_data").then(function (d) {
                $scope.record = d.data;
            }, function (error) {
                alert('Failed');
            });
        }, function () {
            alert('Failed');
        });
    }

    // Onload event
    $scope.load = function (id) {
        $http.get("/Home/GetDataByID?id="+id).then(function (d) {
            $scope.UserModel = d.data[0];
        }, function (error) {
            alert('Failed');
        });
    }

    // Update data in database
    $scope.btnUpdate = "Update";
    $scope.Update = function () {
        $scope.btnSave = "Please Wait....";
        $http({
            method: 'POST',
            url: '/Home/UpdateData',
            data: $scope.UserModel
        }).then(function (d) {
            $scope.btnSave = "Update";
            $scope.UserModel = null;
            alert(d.data);
        }, function () {
            alert('Failed');
        });       
    };
});
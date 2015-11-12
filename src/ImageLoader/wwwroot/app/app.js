var app = angular.module('app', []);

app.controller('homeCtrl', function ($rootScope, $scope, $http, $q) {
    var vm = this;
    vm.currentTpl = '';
    vm.images = [];
    vm.newUrl = 'http://a5.mzstatic.com/us/r30/Purple7/v4/ab/af/3e/abaf3e37-3582-80d0-c489-5fd91ae3b145/icon256.png';
    
    vm.addImage = function () {
        vm.site = $scope.getDomain(vm.newUrl);
        vm.url = vm.newUrl;

        $http.post('/api/ImageApi',vm)
            .then(function (response) {
                vm.images.push(response.data);
            }, function(error) {
                console.log(error.data);
            });
    }

    vm.removeImage = function (image) {
        $http.delete('/api/ImageApi/' + image.id)
            .then(function (response) {
                var index = vm.images.indexOf(image);
                vm.images.splice(index, 1);
            }, function (error) {
                console.log(error.data);
            });
    }

    $scope.getImages = function() {
        $http.get('/api/ImageApi')
            .then(function(response){
                vm.images = response.data;
                vm.currentTpl = '/tpl.html';
            });
    }

    $scope.getImages();

    $scope.getDomain = function (url) {
        var a = document.createElement('a');
        a.href = url;

        var domain = a.hostname;;
        a.remove();

        return domain;
    }
});
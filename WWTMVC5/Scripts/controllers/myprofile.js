﻿wwtng.controller('MyProfile', [
    '$rootScope',
    '$scope',
    'dataproxy',
    '$timeout',
    '$routeParams',
    'UIHelper',
    'FileUploader',
    function ($rootScope, $scope, dataproxy, $timeout, $routeParams, uiHelper, fileUploader) {

        function init() {
            console.log('myprofile init');
            $scope.options = {
                activeTab: 'uploads'
            };
            uiHelper.fixLinks('profileLink');
            dataproxy.requireAuth().then(function (types) {
                $scope.types = types;
                console.log('requireAuth', types);
                dataproxy.getMyProfile(0).then(setProfile, function() {console.log(arguments)});
            }, function(reason) {
                location.reload();
            });
        }

        $scope.uploader = new fileUploader({
            url: "/Entity/AddThumbnail/User",
            autoUpload: true,
            onSuccessItem: success,
            onErrorItem: log,
            alias: 'thumbnail'
        });

        function success(xhr, response) {
            $timeout(function() {
                $scope.editProfile.profileImageId = response.ThumbnailID;

            });
        }

        function log() {
            console.log({ uploader: arguments });
        }

        function setProfile(profile) {
            console.log('setProfile', profile);
            if (typeof profile == 'string' && profile.indexOf('error') === 0) {
                location.reload();
            }
            if (profile.ProfilePhotoLink) {
                if (profile.ProfilePhotoLink.indexOf("~/Content") === 0) {
                    profile.ProfilePhotoLink = profile.ProfilePhotoLink.replace("~/Content/Images", ($rootScope.contentRoot + '/images'));
                } else {
                    profile.ProfilePhotoLink = profile.ProfilePhotoLink + '/true';
                }
            }
            $scope.profile = profile;
            //string affiliation, string aboutMe, bool isSubscribed, Guid? profileImageId, string profileName
            $scope.editProfile = {};
            $scope.editProfile.profileName = profile.ProfileName;
            $scope.editProfile.aboutMe = profile.AboutProfile;
            $scope.editProfile.affiliation = profile.Affiliation,
            $scope.editProfile.profileId = profile.ProfileId;
            $scope.editProfile.isSubscribed = profile.IsSubscribed;
            dataproxy.getUserEntities('Content', profile.ProfileId).then(function (response) {
                console.log('userUploads', response);
                $scope.$applyAsync(function() {
                    $scope.profile.uploads = response.entities && response.entities.length ? response.entities : [];
                    wwt.triggerResize();
                });
            });
            dataproxy.getUserEntities('Community', profile.ProfileId).then(function (response) {
                console.log('userCommunities', response);
                $scope.$applyAsync(function() {
                    $scope.profile.communities = response.entities && response.entities.length ? response.entities : [];
                    wwt.triggerResize();
                });
            });
            getRequests();
        }

        var getRequests = function() {
            dataproxy.getUserRequests().then(function (response) {
                $scope.$applyAsync(function() {
                if (response.Result && response.Result.PermissionItemList) {
                    response = response.Result;
                }
                $scope.profile.requests = response.PermissionItemList && response.PermissionItemList.length ? response.PermissionItemList : null;
                wwt.triggerResize();
                });
            });
        }

        $scope.tabChange = function(tab) {
            $scope.options.activeTab = tab;
            wwt.triggerResize();
        }

        $scope.saveProfile = function() {
            dataproxy.saveProfile($scope.editProfile).then(function(response) {
                console.log(response);
                setProfile(response);
            });
        }
        $scope.rememberMe = wwt.user.get('rememberMe');
        $scope.rememberMeChange = function() {
            wwt.user.set('rememberMe', $scope.rememberMe);
        }
        $scope.logout = function() {
            wwt.user.set('rememberMe', false);
            location.href = '/Logout';
        }

        $scope.approveRequest = function(r) {
            //args: long entityId, long requestorId, UserRole userRole, bool approve
            var row = $('tr[requestHash="' + r.$$hashKey + '"]');
            var roles = ['', 'Reader', 'Contributor', 'Moderator','Owner'];
            var role = roles[parseInt(row.find('select').val())];
            var requestArgs = {
                entityId: r.CommunityId,
                requestorId: r.Id,
                userRole: role,
                approve: true
            }
            dataproxy.requestResponse(requestArgs).then(function (response) {
                console.log({ update: response });
                getRequests();
            });
        }
        $scope.denyRequest = function (r) {
            //args: long entityId, long requestorId, UserRole userRole, bool approve
            var roles = ['', 'Reader', 'Contributor', 'Moderator', 'Owner'];
            var requestArgs = {
                entityId: r.CommunityId,
                requestorId: r.Id,
                userRole: roles[r.Role],
                approve: false
            }
            dataproxy.requestResponse(requestArgs).then(function (response) {
                console.log({ update: response });
                getRequests();
            });
        }

        init();
}]);
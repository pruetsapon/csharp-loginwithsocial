$(function() {
    // display near me section if location enable
    $(".btn-facebook").on('click', function() {
        checkFacebookLogin();
    });
    //attach google login
    gapi.load('auth2', function() {
        const GOOGLE_CLIENT_ID = $('#btn-google').attr('app-id');
        auth2 = gapi.auth2.init({
            client_id: GOOGLE_CLIENT_ID
        });
        attachSignin($("#btn-google")[0]);
    });
});

window.fbAsyncInit = function() {
    const FB_APP_ID = $('#btn-facebook').attr('app-id');
    FB.init({
        appId: FB_APP_ID,
        cookie: true, // enable cookies to allow the server to access 
        xfbml: true, // parse social plugins on this page
        version: 'v2.8' // use graph api version 2.8
    });
};

// Load fb SDK asynchronously
(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s);
    js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

function checkFacebookLogin() {
    FB.getLoginStatus(function(response) {
        if (response.status === 'connected') {
            //login already
            doLoginWithFB(response.authResponse.accessToken);
        } else {
            doFBLogin();
        }
    });
}

function doFBLogin() {
    FB.login(function(response) {
        if (response.status != 'not_authorized') {
            doLoginWithFB(response.authResponse.accessToken);
        }
    }, { scope: 'email' });
}

// Here we run a very simple test of the Graph API after login is
// successful.  See statusChangeCallback() for when this call is made.
function doLoginWithFB(accessToken) {
    FB.api('/me', { fields: 'id, name, first_name, last_name, email, picture.width(100)' }, function(response) {
        if (response.email == undefined) {
            FB.api("/me/permissions", "DELETE");
        } else {
            var loginData = {
                FacebookId: response.id,
                Name: response.first_name,
                Lastname: response.last_name,
                Email: response.email,
                AccessToken: accessToken
            };
            if (response.picture.data.is_silhouette != true) {
                loginData.Image = response.picture.data.url;
            }
            $.ajax({
                url: window.location.protocol + "//" + window.location.host + '/api/facebook/login/',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(loginData),
                success: function() {
                    alert("login success!!");
                }
            });
            $('#loginModel').modal('hide');
            $('#confirmEmail').modal('hide');
        }
    });
}

//add event handle to button and trig google login
function attachSignin(element) {
    auth2.attachClickHandler(element, {},
        function(googleUser) {
            var profile = googleUser.getBasicProfile();
            var response = googleUser.getAuthResponse();
            doLoginWithGoogle(profile, response.id_token);
        },
        function(error) {
            // console.log(error);
        }
    );
}

// google login succesfully
function doLoginWithGoogle(userData, accessToken) {
    var loginData = {
        Name: userData.ofa,
        Lastname: userData.wea,
        Email: userData.U3,
        Image: userData.Paa,
        AccessToken: accessToken
    };
    $.ajax({
        url: window.location.protocol + "//" + window.location.host + '/api/google/login/',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(loginData),
        success: function() {
            alert("login success!!");
        }
    });
    $('#loginModel').modal('hide');
}
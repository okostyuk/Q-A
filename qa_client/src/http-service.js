const http_service = {
    baseUrl: "",//https://localhost:5001",
    DELETE(path, listener) {
        this.proceed(fetch(
            this.baseUrl + path, 
            {method: 'DELETE'}),
            listener
        )
    },
    GET(path, listener) {
        setTimeout(() => this.proceed(fetch(this.baseUrl + path), listener), 250);
    },
    POST(path, listener, bodyObject) {
        console.log(JSON.stringify(bodyObject));
        setTimeout(() =>
        this.proceed(
            fetch(this.baseUrl + path, {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(bodyObject)}),
            listener
        ), 250);
    },
    proceed(promise, listener) {
        promise.then(response => {
            if (response.status !== 200) {
                response.json().then(jsonResponse => listener.onError("Response error: " + response.status + " " + JSON.stringify(jsonResponse)));
            } else {
                response.json().then(
                    jsonResponse => {
                        console.log(jsonResponse);
                        if (jsonResponse.authError !== undefined) {
                            listener.onAuthError(jsonResponse.authError);
                        }else if (jsonResponse.error !== undefined && jsonResponse.error !== null) {
                            console.log("http_service listener.onError");
                            listener.onError("Response error: " + jsonResponse.error);
                        } else {
                            console.log("http_service listener.onSuccess");
                            listener.onSuccess(jsonResponse.result);
                        }
                    }
                )
            }
        },
            reject => {
                console.log(reject);
                listener.onError(reject);
            })
    },
    logout() {
        this.POST('/api/auth/logout', {}, {});
        this.deleteAllCookies();
        document.cookie = "";
    },
    email() {
        return unescape(this.getCookie("qa_user_email"));
    },
    userId() {
        return this.getCookie("qa_user_id");
    },
    getCookie(cname) {
        let name = cname + "=";
        let ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    },
    deleteAllCookies() {
        let cookies = document.cookie.split(";");
    
        for (let i = 0; i < cookies.length; i++) {
            let cookie = cookies[i];
            let eqPos = cookie.indexOf("=");
            let name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }
    }
};

export default http_service

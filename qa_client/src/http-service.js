const http_service = {
    baseUrl: "",
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
                        if (jsonResponse.error !== undefined && jsonResponse.error !== null) {
                            console.log("http_service listener.onError");
                            listener.onError("Response error: " + jsonResponse.error);
                        } else {
                            console.log("http_service listener.onSuccess");
                            listener.onSuccess(jsonResponse);
                        }
                    }
                )
            }
        },
            reject => {
                console.log(reject);
                listener.onError(reject);
            })
    }
};

export default http_service

const http_service = {
    baseUrl: "https://192.168.1.128:5001",
    GET(path, listener) {
        setTimeout(() => this.proceed(fetch(this.baseUrl + path), listener), 500);
    },
    POST(path, listener, bodyObject) {
        setTimeout(() =>
        this.proceed(
            fetch(this.baseUrl + path, {
                    method: 'POST',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(bodyObject)}),
            listener
        ), 1000);
    },
    proceed(promise, listener) {
        promise.then(response => {
            if (response.status !== 200) {
                listener.onError("Response error: " + response.status);
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

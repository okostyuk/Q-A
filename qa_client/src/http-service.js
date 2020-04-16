const http_service = {
    GET(path, listener) {
        setTimeout(() => this.proceed(fetch(path), listener), 500);
    },
    POST(path, listener, bodyObject) {
        setTimeout(() =>
        this.proceed(
            fetch(path, {
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
        })
    }
};

export default http_service

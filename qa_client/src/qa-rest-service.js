import http_service from "./http-service";
const qa_rest_service = {

    /**
     * @param {{email:string password:string}} authRequest
     */
    login(authRequest) {
        //let qa_rest_service = this;
        return new Promise(function (resolve, reject) {
            let httpRequestPromise = http_service.SIMPLE_POST('/api/auth', authRequest);
            qa_rest_service.proceed(httpRequestPromise, resolve, reject);
        });
    },
    
    /**
     * @param {{email:string password:string}} authRequest
     */
    register(authRequest) {
        return new Promise(function (resolve, reject) {
            let httpRequestPromise = http_service.SIMPLE_POST('/api/auth/signUp', authRequest);
            qa_rest_service.proceed(httpRequestPromise, resolve, reject);
        });
    },
    
    getQuestions() {
        return new Promise(function (resolve, reject) {
            let httpRequestPromise = http_service.GET('/api/questions');
            qa_rest_service.proceed(httpRequestPromise, resolve, reject);
        });
    },
    
    getMyQuestions() {
        return new Promise(function (resolve, reject) {
            let httpRequestPromise = http_service.GET('/api/questions/my');
            qa_rest_service.proceed(httpRequestPromise, resolve, reject);
        });
    },
    
    /**
     * @param questionId:string
     */
    getQuestion(questionId) {
        return new Promise(function (resolve, reject) {
            let httpRequestPromise = http_service.GET('/api/questions' + questionId);
            qa_rest_service.proceed(httpRequestPromise, resolve, reject);
        });
    },

    proceed(promise, resolve, reject) {
        promise.then(response => {
            if (response.ok) {
                response.json().then(jsonBody => {
                    if (jsonBody.result !== undefined) {
                        resolve(jsonBody.result);
                    } else if (jsonBody.authError !== undefined) {
                        reject("AUTH_ERROR")
                    } else if (jsonBody.error !== undefined) {
                        reject(jsonBody.error)
                    } else {
                        reject("result is empty");
                    }                    
                });
            } else {
                reject("HTTP error: " + response.status);
            }
        }).catch(reason => {
            reject("Network error: " + reason);
        });
    }
};
export default qa_rest_service
